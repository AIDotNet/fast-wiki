---
sidebar_position: 3
---

# 模板化你的Prompts

在[上一篇文章](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt)中，我们创建了一个用于获取用户意图的Prompts。然而，这个功能并不是很可重用。因为选项是硬编码的。我们可以动态创建Prompts字符串，但有一个更好的方法：Prompts模板。

通过本示例，您将学习如何将Prompts模板化。如果您想查看最终解决方案，可以查看公共文档存储库中的以下示例。如果您想跟着做，请使用上一个解决方案的链接。

| 语言   | 链接到上一个解决方案                                    | 链接到最终解决方案                                       |
| :----- | :-------------------------------------------------------- | :-------------------------------------------------------- |
| C#     | [在GitHub中打开示例](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/DocumentationExamples/Templates.cs) | [在GitHub中打开解决方案](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/KernelSyntaxExamples/Example31_SerializingPrompts.cs) |
| Python | [在GitHub中打开解决方案](https://github.com/MicrosoftDocs/semantic-kernel-docs/tree/main/samples/python/07-Serializing-Prompts) | [在GitHub中打开解决方案](https://github.com/MicrosoftDocs/semantic-kernel-docs/tree/main/samples/python/04-Templatizing-Prompts) |

## 将变量添加到Prompts中

使用Semantic Kernel的模板语言，我们可以添加将自动替换为输入参数的令牌。首先，让我们构建一个非常简单的Prompts，该Prompts使用Semantic Kernel模板语法语言，包含足够的信息，以便代理可以回复用户。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts?tabs=Csharp#tabpanel_1_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts?tabs=Csharp#tabpanel_1_python)

C#

```csharp
// 创建用于聊天的Semantic Kernel模板
var chat = kernel.CreateFunctionFromPrompt(
    @"{{$history}}
    User: {{$request}}
    Assistant: ");
```

新的Prompts使用`request`和`history`变量，这样我们在运行Prompts时可以包含这些值。为了测试我们的Prompts，我们可以创建一个聊天循环，这样我们就可以和我们的代理开始来回交谈。当我们调用Prompts时，我们可以将`request`和`history`变量作为参数传入。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts?tabs=Csharp#tabpanel_2_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts?tabs=Csharp#tabpanel_2_python)

C#

```csharp
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

        Kernel kernel = Kernel.CreateBuilder()
                              .AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey)
                              .Build();

        ChatHistory history = new();

        // 开始聊天循环
        while (true)
        {
            // 获取用户输入
            Write("User > ");
            var request = ReadLine();

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

            // 添加到历史记录
            history.AddUserMessage(request!);
            history.AddAssistantMessage(message);
        }
```

## 使用Handlebars模板引擎

除了核心模板语法之外，Semantic Kernel还支持C# SDK中的Handlebars模板语言。要使用Handlebars，您首先需要将Handlebars包添加到项目中。

控制台

```console
dotnet add package Microsoft.SemanticKernel.PromptTemplate.Handlebars --prerelease
```

然后导入Handlebars模板引擎包。

C#

```csharp
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
```

之后，您可以使用`HandlebarsPromptTemplateFactory`创建一个新的Prompts。因为Handlebars支持循环，我们可以使用它来循环遍历示例和聊天历史等元素。这使得它非常适合我们在[上一篇文章](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt)中创建的`getIntent`Prompts。

C#

```csharp
        // 为意图创建handlebars模板
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

{{#each chatHistory}}
    <message role=""{{role}}"">{{content}}</message>
{{/each}}

<message role=""user"">{{request}}</message>
<message role=""system"">Intent:</message>",
                TemplateFormat = "handlebars"
            },
            new HandlebarsPromptTemplateFactory()
        );
```

然后，您可以创建将在模板中使用的选择和示例对象。在这个例子中，我们可以使用我们的Prompts来在对话结束时结束对话。为此，我们只需提供两个有效的意图：`ContinueConversation`和`EndConversation`。

C#

```csharp
// 创建选择
List<string> choices = new() { "ContinueConversation", "EndConversation" };

// 创建few-shot示例
List<ChatHistory> fewShotExamples =
[
    new ChatHistory()
    {
        new ChatMessageContent(AuthorRole.User, "Can you send a very quick approval to the marketing team?"),
        new ChatMessageContent(AuthorRole.System, "Intent:"),
        new ChatMessageContent(AuthorRole.Assistant, "ContinueConversation")
    },
    new ChatHistory()
    {
        new ChatMessageContent(AuthorRole.User, "Thanks, I'm done for now"),
        new ChatMessageContent(AuthorRole.System, "Intent:"),
        new ChatMessageContent(AuthorRole.Assistant, "EndConversation")
    }
];
```

最后，您可以使用内核运行Prompts。将以下代码添加到您的主聊天循环中，以便在意图为`EndConversation`时终止循环。

C#

```csharp
// 调用Prompts
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

// 如果意图为"EndConversation"，则结束聊天
if (intent.ToString() == "EndConversation")
{
    break;
}
```

## 迈出下一步

现在您可以将您的Prompts模板化，您现在可以学习如何从Prompts中调用函数，以帮助将Prompts分解为更小的部分。
