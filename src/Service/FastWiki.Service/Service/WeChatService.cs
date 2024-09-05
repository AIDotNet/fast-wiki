using System.Xml;
using FastWiki.Service.Backgrounds;
using FastWiki.Service.Contracts.WeChat;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace FastWiki.Service.Service;

/// <summary>
///     微信服务
/// </summary>
public class WeChatService
{
    private const string OutputTemplate =
        """
        您好，欢迎关注！
        由于微信限制，我们无法立即回复您的消息，但是您的消息已经收到，我们会尽快回复您！
        如果获取消息结果，请输入继续。
        """;

    /// <summary>
    ///     接收消息
    /// </summary>
    /// <param name="context"></param>
    /// <param name="id"></param>
    /// <param name="memoryCache"></param>
    public static async Task ReceiveMessageAsync(HttpContext context, string id, IMemoryCache memoryCache)
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
            return;


        if (id == null)
        {
            context.Response.ContentType = "application/xml";
            await context.Response.WriteAsync(GetOutputXml(output, "参数错误，请联系管理员！code:id_null"));
            return;
        }

        var messageId = GetMessageId(output);

        string outputValue;

        if (output.Content.Trim().Equals("继续", StringComparison.OrdinalIgnoreCase))
        {
            // 尝试4s
            for (var i = 0; i < 9; i++)
            {
                await Task.Delay(500);

                Console.WriteLine($"Try to get message from cache: {messageId}");

                // 从缓存中获取,如果有则返回
                memoryCache.TryGetValue(messageId, out var value);

                Log.Information($"Try to get message from cache: {messageId}, value: {value} Type: {value?.GetType()}");

                if (value is string s && !s.IsNullOrEmpty())
                {
                    await WriteMessageAsync(context, output, s, messageId, true);
                    return;
                }
            }

            await WriteMessageAsync(context, output, "抱歉，暂时没有消息，请稍后在回复", messageId);
            return;
        }

        // 先写入channel，等待后续处理
        WeChatBackgroundService.Channel.Writer.TryWrite(new WeChatAI
        {
            Content = output.Content,
            SharedId = id,
            MessageId = messageId
        });

        // 等待4s
        for (var i = 0; i < 9; i++)
        {
            await Task.Delay(500);
            if (!memoryCache.TryGetValue(messageId, out outputValue) || outputValue.IsNullOrEmpty())
                continue;

            await WriteMessageAsync(context, output, outputValue, messageId, true);
            return;
        }

        // 尝试从缓存中获取
        memoryCache.TryGetValue(messageId, out outputValue);
        if (!outputValue.IsNullOrEmpty())
        {
            await WriteMessageAsync(context, output, outputValue, messageId, true);
            return;
        }

        await WriteMessageAsync(context, output, OutputTemplate, messageId);
    }

    public static async Task WriteMessageAsync(HttpContext context, WehCahtMe chatAi, string outputValue,
        string messageId = null, bool remove = false)
    {
        context.Response.ContentType = "application/xml";
        await context.Response.WriteAsync(GetOutputXml(chatAi, outputValue));
        if (!messageId.IsNullOrWhiteSpace() && remove)
            context.RequestServices.GetRequiredService<IMemoryCache>().Remove(messageId);
    }

    private static string GetMessageId(WehCahtMe output)
    {
        return output.FromUserName + output.ToUserName;
    }

    /// <summary>
    ///     获取返回的xml
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