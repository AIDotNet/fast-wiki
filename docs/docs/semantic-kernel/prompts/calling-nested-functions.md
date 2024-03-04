---
sidebar_position: 4
---

# 在prompt内调用functions

在上一篇文章中，我们演示了如何[模板化Prompts](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts)以使其更具重复使用性。在本文中，我们将向您展示如何在Prompts中调用其他函数*内部*，以帮助将Prompts分解为更小的部分。这有助于保持LLM专注于单个任务，避免击中令牌限制，并允许您直接将原生代码添加到您的Prompts中。

如果您想查看最终解决方案，可以在公共文档存储库中查看以下示例。如果您想跟着学习，请使用前一个解决方案的链接。

| 语言   | 链接到上一个解决方案                                    | 链接到最终解决方案                                       |
| :----- | :-------------------------------------------------------- | :-------------------------------------------------------- |
| C#     | [在GitHub中打开示例](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/DocumentationExamples/Templates.cs) | [在GitHub中打开解决方案](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/DocumentationExamples/FunctionsWithinPrompts.cs) |
| Python | [在GitHub中打开解决方案](https://github.com/MicrosoftDocs/semantic-kernel-docs/tree/main/samples/python/04-Templatizing-Prompts) | [在GitHub中打开解决方案](https://github.com/MicrosoftDocs/semantic-kernel-docs/tree/main/samples/python/05-Nested-Functions-In-Prompts) |

## 调用嵌套functions

在[之前的示例](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts)中，我们创建了一个与用户交谈的Prompts。该函数使用以前的对话历史记录来确定代理应该接下来说什么。

然而，将整个历史记录放入单个Prompts中可能会导致使用太多令牌。为了避免这种情况，我们可以在询问意图之前总结对话历史。为此，我们可以利用作为[核心插件包](https://learn.microsoft.com/zh-cn/semantic-kernel/agents/plugins/out-of-the-box-plugins)一部分的`ConversationSummaryPlugin`。

下面，我们将展示如何更新我们的原始Prompts以使用`ConversationSummaryPlugin`中的`SummarizeConversation`函数来总结对话历史，然后再询问意图。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/calling-nested-functions?tabs=Csharp#tabpanel_1_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/calling-nested-functions?tabs=Csharp#tabpanel_1_python)

C#

```csharp
        var chat = kernel.CreateFunctionFromPrompt(
@"{{ConversationSummaryPlugin.SummarizeConversation $history}}
User: {{$request}}
Assistant: "
        );
```

## 测试更新后的Prompts

在添加嵌套函数后，您必须确保将所需函数加载到内核中。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/calling-nested-functions?tabs=Csharp#tabpanel_2_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/calling-nested-functions?tabs=Csharp#tabpanel_2_python)

C#

```csharp
var builder = Kernel.CreateBuilder()
                    .AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
builder.Plugins.AddFromType<ConversationSummaryPlugin>();
Kernel kernel = builder.Build();
```

之后，我们可以通过创建一个聊天循环来测试Prompts，使历史记录逐渐变长。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/calling-nested-functions?tabs=Csharp#tabpanel_3_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/calling-nested-functions?tabs=Csharp#tabpanel_3_python)

C#

```csharp
// 创建聊天历史记录
ChatHistory history = new();

// 启动聊天循环
while (true)
{
    // 获取用户输入
    Write("User > ");
    var request = ReadLine();

    // 调用handlebarsPrompts
    var intent = await kernel.InvokeAsync(
        getIntent,
        new()
        {
            { "request", request },
            { "choices", choices },
            { "history", history },
            { "fewShotExamples", fewShotExamples }
        }
    );

    // 如果意图是“停止”，则结束聊天
    if (intent.ToString() == "EndConversation")
    {
        break;
    }

    // 获取聊天响应
    var chatResult = kernel.InvokeStreamingAsync<StreamingChatMessageContent>(
        chat,
        new()
        {
            { "request", request },
            { "history", string.Join("\n", history.Select(x => x.Role + ": " + x.Content)) }
        }
    );

    // 流式传输响应
    string message = "";
    await foreach (var chunk in chatResult)
    {
        if (chunk.Role.HasValue)
        {
            Write(chunk.Role + " > ");
        }
        message += chunk;
        Write(chunk);
    }
    WriteLine();

    // 追加到历史记录
    history.AddUserMessage(request!);
    history.AddAssistantMessage(message);
}
```

## 在Handlebars中调用嵌套函数

在上一篇文章中，我们展示了如何使用Handlebars模板引擎创建`getIntent`Prompts。在本文中，我们将向您展示如何使用相同的嵌套函数更新此Prompts。

与之前的示例类似，我们可以使用`SummarizeConversation`函数在询问意图之前总结对话历史。唯一的区别是，我们需要使用Handlebars语法来调用函数，这要求我们在插件名称和函数名称之间使用`-`而不是`.`。

C#

```csharp
        var getIntent = kernel.CreateFunctionFromPrompt(
            new()
            {
                Template = @"
<message role=""system"">Instructions: What is the intent of this request?
Do not explain the reasoning, just reply back with the intent. If you are unsure, reply with {{choices[0]}}.
Choices: {{choices}}.</message>

{{#each fewShotExamples}}
    {{#each this}}
        <message role=""{{role}}"">{{content}}</message>
    {{/each}}
{{/each}}

{{ConversationSummaryPlugin-SummarizeConversation history}}

<message role=""user"">{{request}}</message>
<message role=""system"">Intent:</message>",
                TemplateFormat = "handlebars"
            },
            new HandlebarsPromptTemplateFactory()
        );
```

## 迈出下一步

现在您可以调用嵌套函数，您现在可以学习如何[配置您的Prompts](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/configure-prompts)。