using System.Text;
using System.Threading.Channels;
using System.Xml;
using FastWiki.Service.Contracts.OpenAI;
using FastWiki.Service.Contracts.WeChat;
using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Infrastructure.Helper;
using Masa.BuildingBlocks.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using TokenApi.Service.Exceptions;

namespace FastWiki.Service.Service;

/// <summary>
/// 微信服务
/// </summary>
public class WeChatService
{
    static WeChatService()
    {
        Task.Run(AIChatAsync);
    }


    private static readonly Channel<WeChatAI> Channel = System.Threading.Channels.Channel.CreateUnbounded<WeChatAI>();

    private const string OutputTemplate =
        """
        您好，欢迎关注FastWiki！
        由于微信限制，我们无法立即回复您的消息，但是您的消息已经收到，我们会尽快回复您！
        如果获取消息结果，请输入1。
        如果您有其他问题，可以直接回复，我们会尽快回复您！
        """;

    public static async Task AIChatAsync()
    {
        using var scope = MasaApp.RootServiceProvider.CreateScope();

        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        var wikiMemoryService = scope.ServiceProvider.GetRequiredService<WikiMemoryService>();
        var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

        while (await Channel.Reader.WaitToReadAsync())
        {
            var content = await Channel.Reader.ReadAsync();

            await SendMessageAsync(content, eventBus, wikiMemoryService, memoryCache);
        }
    }

    /// <summary>
    /// 微信AI对话
    /// </summary>
    /// <param name="chatAi"></param>
    /// <param name="eventBus"></param>
    /// <param name="wikiMemoryService"></param>
    /// <param name="memoryCache"></param>
    public static async Task SendMessageAsync(WeChatAI chatAi, IEventBus eventBus,
        WikiMemoryService wikiMemoryService, IMemoryCache memoryCache)
    {
        var chatShareInfoQuery = new ChatShareInfoQuery(chatAi.SharedId);

        await eventBus.PublishAsync(chatShareInfoQuery);

        // 如果chatShareId不存在则返回让下面扣款
        var chatShare = chatShareInfoQuery.Result;

        var chatApplicationQuery = new ChatApplicationInfoQuery(chatShareInfoQuery.Result.ChatApplicationId);

        await eventBus.PublishAsync(chatApplicationQuery);

        var chatApplication = chatApplicationQuery?.Result;

        if (chatApplication == null)
        {
            return;
        }

        int requestToken = 0;

        var module = new ChatCompletionDto<ChatCompletionRequestMessage>()
        {
            messages =
            [
                new()
                {
                    content = chatAi.Content,
                    role = "user",
                }
            ]
        };

        var chatHistory = new ChatHistory();

        // 如果设置了Prompt，则添加
        if (!chatApplication.Prompt.IsNullOrEmpty())
        {
            chatHistory.AddSystemMessage(chatApplication.Prompt);
        }

        // 保存对话提问
        var createChatRecordCommand = new CreateChatRecordCommand(chatApplication.Id, chatAi.Content);

        await eventBus.PublishAsync(createChatRecordCommand);

        var sourceFile = new List<FileStorage>();
        var memoryServerless = wikiMemoryService.CreateMemoryServerless(chatApplication.ChatModel);

        // 如果为空则不使用知识库
        if (chatApplication.WikiIds.Count != 0)
        {
            var success = await OpenAIService.WikiPrompt(chatApplication, memoryServerless, chatAi.Content, eventBus,
                sourceFile, module);

            if (!success)
            {
                return;
            }
        }

        var output = new StringBuilder();

        // 添加用户输入，并且计算请求token数量
        module.messages.ForEach(x =>
        {
            if (x.content.IsNullOrEmpty()) return;
            requestToken += TokenHelper.ComputeToken(x.content);

            chatHistory.Add(new ChatMessageContent(new AuthorRole(x.role), x.content));
        });


        if (chatShare != null)
        {
            // 如果token不足则返回，使用token和当前request总和大于可用token，则返回
            if (chatShare.AvailableToken != -1 &&
                (chatShare.UsedToken + requestToken) >=
                chatShare.AvailableToken)
            {
                output.Append("Token不足");
                return;
            }

            // 如果没有过期则继续
            if (chatShare.Expires != null &&
                chatShare.Expires < DateTimeOffset.Now)
            {
                output.Append("Token已过期");
                return;
            }
        }


        try
        {
            await foreach (var item in OpenAIService.SendChatMessageAsync(chatApplication, eventBus, wikiMemoryService,
                               chatHistory))
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }

                output.Append(item);
            }

            //对于对话扣款
            if (chatShare != null)
            {
                var updateChatShareCommand = new DeductTokenCommand(chatShare.Id,
                    requestToken);

                await eventBus.PublishAsync(updateChatShareCommand);
            }
        }
        catch (NotModelException notModelException)
        {
            output.Clear();
            output.Append(notModelException.Message);
        }
        catch (InvalidOperationException invalidOperationException)
        {
            output.Clear();
            output.Append("对话异常:" + invalidOperationException.Message);
        }
        catch (ArgumentException argumentException)
        {
            output.Clear();
            output.Append("对话异常:" + argumentException.Message);
        }
        catch (Exception e)
        {
            output.Clear();
            output.Append("对话异常,请联系管理员");
        }
        finally
        {
            memoryCache.Set(chatAi.MessageId, output.ToString(), TimeSpan.FromMinutes(5));
        }
    }

    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="context"></param>
    public static async Task ReceiveMessageAsync(HttpContext context, string? id, IMemoryCache memoryCache)
    {
        if (context.Request.Method != "POST")
        {
            context.Request.Query.TryGetValue("signature", out var signature);
            context.Request.Query.TryGetValue("timestamp", out var timestamp);
            context.Request.Query.TryGetValue("nonce", out var nonce);
            context.Request.Query.TryGetValue("echostr", out var echostr);
            await context.Response.WriteAsync(echostr);
            return;
        }

        using var reader = new StreamReader(context.Request.Body);
        // xml解析
        var body = await reader.ReadToEndAsync();
        var doc = new XmlDocument();
        doc.LoadXml(body);
        var root = doc.DocumentElement;
        var input = new WeChatMessageInput
        {
            ToUserName = root.SelectSingleNode("ToUserName")?.InnerText,
            FromUserName = root.SelectSingleNode("FromUserName")?.InnerText,
            CreateTime = long.Parse(root.SelectSingleNode("CreateTime")?.InnerText ?? "0"),
            MsgType = root.SelectSingleNode("MsgType")?.InnerText,
            Content = root.SelectSingleNode("Content")?.InnerText,
            MsgId = long.Parse(root.SelectSingleNode("MsgId")?.InnerText ?? "0")
        };

        var output = new WehCahtMe
        {
            ToUserName = input.ToUserName,
            FromUserName = input.FromUserName,
            CreateTime = input.CreateTime,
            MsgType = input.MsgType,
            Content = input.Content
        };

        if (output.Content.IsNullOrEmpty())
        {
            return;
        }


        if (id == null)
        {
            context.Response.ContentType = "application/xml";
            await context.Response.WriteAsync(GetOutputXml(output, "参数错误，请联系管理员！code:id_null"));
            return;
        }

        string outputValue = string.Empty;

        var messageId = GetMessageId(output);

        // 从缓存中获取,如果有则返回
        memoryCache.TryGetValue(messageId, out var value);

        // 如果value有值则，但是value为空，则返回提示,防止重复提问！
        if (value is string str && str.IsNullOrEmpty())
        {
            await WriteMessageAsync(context, output, "暂无消息，请稍后再试！code:no_message");
            return;
        }
        else if (value is string v && !v.IsNullOrEmpty())
        {
            await WriteMessageAsync(context, output, v, messageId);
            return;
        }

        if (output.Content == "1")
        {
            if (value is string v && !v.IsNullOrEmpty())
            {
                await WriteMessageAsync(context, output, v, messageId);
                return;
            }

            await WriteMessageAsync(context, output, "暂无消息，请稍后再试！code:no_message");
            return;
        }

        // 先写入channel，等待后续处理
        Channel.Writer.TryWrite(new WeChatAI()
        {
            Content = output.Content,
            SharedId = id,
            MessageId = messageId
        });

        // 等待4s
        for (int i = 0; i < 9; i++)
        {
            await Task.Delay(510);
            if (!memoryCache.TryGetValue(messageId, out outputValue) || outputValue.IsNullOrEmpty()) continue;

            await WriteMessageAsync(context, output, outputValue, messageId);
            return;
        }

        // 尝试从缓存中获取
        memoryCache.TryGetValue(messageId, out outputValue);
        if (!outputValue.IsNullOrEmpty())
        {
            await WriteMessageAsync(context, output, outputValue, messageId);
            return;
        }

        context.Response.ContentType = "application/xml";
        await context.Response.WriteAsync(GetOutputXml(output, OutputTemplate));

        // 写入缓存,5分钟过期
        memoryCache.Set(messageId, OutputTemplate, TimeSpan.FromMinutes(5));
    }

    public static async Task WriteMessageAsync(HttpContext context, WehCahtMe chatAi, string outputValue,
        string? messageId = null)
    {
        context.Response.ContentType = "application/xml";
        await context.Response.WriteAsync(GetOutputXml(chatAi, outputValue));
        if (!messageId.IsNullOrWhiteSpace())
        {
            context.RequestServices.GetRequiredService<IMemoryCache>().Remove(messageId);
        }
    }

    private static string GetMessageId(WehCahtMe output)
    {
        return output.FromUserName + output.ToUserName;
    }

    /// <summary>
    /// 获取返回的xml
    /// </summary>
    /// <param name="output"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string GetOutputXml(WehCahtMe output, string content)
    {
        var createTime = DateTimeOffset.Now.ToUnixTimeSeconds();

        var xml =
            $@"
  <xml>
    <ToUserName><![CDATA[{output.FromUserName}]]></ToUserName>
    <FromUserName><![CDATA[{output.ToUserName}]]></FromUserName>
    <CreateTime>{createTime}</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[{content}]]></Content>
  </xml>
";

        return xml;
    }

    public class WeChatMessageInput
    {
        public string URL { get; set; }
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public long CreateTime { get; set; }
        public string MsgType { get; set; }
        public string Content { get; set; }
        public long MsgId { get; set; }
    }

    public class WehCahtMe
    {
        public string ToUserName { get; set; }

        public string FromUserName { get; set; }

        public long CreateTime { get; set; }

        public string MsgType { get; set; }

        public string Content { get; set; }
    }
}