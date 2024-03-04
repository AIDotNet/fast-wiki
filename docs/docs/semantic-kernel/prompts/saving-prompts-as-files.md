---
sidebar_position: 6
---

# 保存和共享prompts

在以前的文章中，我们演示了如何[创建和运行内联提示](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts)。然而，在大多数情况下，您可能会希望将提示创建在一个单独的文件中，以便可以轻松地将它们导入到跨多个项目中的Semantic Kernel，并与他人共享。

在本文中，我们将演示如何创建一个提示所需的文件，以便您可以轻松地将它们导入到Semantic Kernel中。在本文中的一个示例中，我们将在[之前的教程](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts)的基础上，演示如何序列化聊天提示。此提示将被称为`chat`。

如果您想要查看最终解决方案，您可以在公共文档存储库中查看以下示例。如果您想跟着做，可以使用上一个解决方案的链接。


| 语言   | 链接到上一个解决方案                                                                                                                        | 链接到最终解决方案                                                                                                                                                  |
| :----- | :------------------------------------------------------------------------------------------------------------------------------------------ | :------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| C#     | [在GitHub中打开示例](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/DocumentationExamples/FunctionsWithinPrompts.cs) | [在GitHub中打开解决方案](Open example in GitHub](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/DocumentationExamples/SerializingPrompts.cs) |
| Python | [在GitHub中打开解决方案](https://github.com/MicrosoftDocs/semantic-kernel-docs/tree/main/samples/python/05-Nested-Functions-In-Prompts)     | [在GitHub中打开解决方案](https://github.com/MicrosoftDocs/semantic-kernel-docs/tree/main/samples/python/07-Serializing-Prompts)                                     |

## 为您的提示创建一个家

在为`chat`函数创建文件之前，您首先必须定义一个将保存所有插件的文件夹。这将使您以后更容易将它们导入到Semantic Kernel中。我们建议将此文件夹放在项目的根目录，并将其命名为*Prompts*。

在*Prompts*文件夹中，您可以为您的函数创建一个名为*chat*的嵌套文件夹。

目录

```目录
Prompts
│
└─── chat
```

## 为您的提示创建文件

一旦进入提示文件夹，您需要创建两个新文件：*skprompt.txt*和*config.json*。*skprompt.txt*文件包含将发送到AI服务的提示，而*config.json*文件包含配置以及可以被规划器使用的语义描述。

请在*chat*文件夹中创建这两个文件。

目录

```目录
Prompts
│
└─── chat
     |
     └─── config.json
     └─── skprompt.txt
```

### 在*skprompt.txt*文件中编写提示

*skprompt.txt*文件包含将发送到AI服务的请求。由于我们已经在[之前的教程](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts)中编写了提示，我们可以简单地将其复制到*skprompt.txt*文件中。

txt

```txt
{{ConversationSummaryPlugin.SummarizeConversation $history}}
User: {{$request}}
Assistant:
```

### 在*config.json*文件中配置函数

接下来，我们需要为`chat`函数定义配置。在序列化配置时，您只需要在JSON文件中定义相同的属性：

- `type` – 提示的类型。在这种情况下，我们使用`completion`类型。
- `description` – 提示的描述。这将被规划器用于自动编排与函数相关的计划。
- `completion` – 完成模型的设置。对于OpenAI模型，这包括`max_tokens`和`temperature`属性。
- `input` – 定义提示内部使用的变量（例如`input`）。

对于`chat`函数，我们可以使用之前相同的配置[如前所述](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/configure-prompts)。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/saving-prompts-as-files?tabs=Csharp#tabpanel_1_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/saving-prompts-as-files?tabs=Csharp#tabpanel_1_python)

JSON

```json
{
     "schema": 1,
     "type": "completion",
     "description": "Creates a chat response to the user",
     "execution_settings": {
        "default": {
          "max_tokens": 1000,
          "temperature": 0
        },
        "gpt-3.5-turbo": {
          "model_id": "gpt-3.5-turbo-0613",
          "max_tokens": 4000,
          "temperature": 0.1
        },
        "gpt-4": {
          "model_id": "gpt-4-1106-preview",
          "max_tokens": 8000,
          "temperature": 0.3
        }
      },
     "input_variables": [
        {
          "name": "request",
          "description": "The user's request.",
          "required": true
        },
        {
          "name": "history",
          "description": "The history of the conversation.",
          "required": true
        }
     ]
}
```

### 测试您的提示

此时，您可以通过更新*Program.cs*或*main.py*文件来导入和测试您的函数与内核。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/saving-prompts-as-files?tabs=Csharp#tabpanel_2_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/saving-prompts-as-files?tabs=Csharp#tabpanel_2_python)

```csharp
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Plugins.Core;

        var builder = Kernel.CreateBuilder()
                            .AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
        builder.Plugins.AddFromType<ConversationSummaryPlugin>();
        Kernel kernel = builder.Build();

        // Load prompts
        var prompts = kernel.CreatePluginFromPromptDirectory("./../../../Plugins/Prompts");

        // Create chat history
        ChatHistory history = new();

        // Start the chat loop
        Write("User > ");
        string? userInput;
        while ((userInput = ReadLine()) != null)
        {

            // Get chat response
            var chatResult = kernel.InvokeStreamingAsync<StreamingChatMessageContent>(
                prompts["chat"],
                new()
                {
                    { "request", userInput },
                    { "history", string.Join("\n", history.Select(x => x.Role + ": " + x.Content)) }
                }
            );

            // Stream the response
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

            // Append to history
            history.AddUserMessage(userInput);
            history.AddAssistantMessage(message);

            // Get user input again
            Write("User > ");
        }
```

## 使用YAML来序列化您的提示

除了*skprompt.txt*和*config.json*文件外，您还可以使用C# SDK来使用单个YAML文件来序列化您的提示。如果您想要使用单个文件来定义您的提示，这将非常有用。此外，这是Azure AI Studio使用的相同格式，这样可以更容易地在两个平台之间共享提示。

让我们尝试为`getIntent`提示创建一个YAML序列化文件。首先，您需要安装必要的软件包。

```console
dotnet add package Microsoft.SemanticKernel.Yaml --prerelease
```

本教程还使用了Handlebars模板引擎，因此您还需要安装Handlebars包。

```console
dotnet add package Microsoft.SemanticKernel.PromptTemplate.Handlebars --prerelease
```

接下来，在*Prompts*文件夹中创建一个名为*getIntent.prompt.yaml*的新文件，并将以下YAML内容复制到文件中。

```yaml
name: getIntent
description: 获取用户的意图。
template: |
  <message role="system">说明：这个请求的意图是什么？
  不要解释原因，只需回复意图。如果不确定，使用{{choices[0]}}回复。
  选择：{{choices}}。</message>

  {{#each fewShotExamples}}
      {{#each this}}
          <message role="{{role}}">{{content}}</message>
      {{/each}}
  {{/each}}

  {{ConversationSummaryPlugin.SummarizeConversation history}}

  <message role="user">{{request}}</message>
  <message role="system">意图：</message>
template_format: handlebars
input_variables:
  - name:          choices
    description:   AI可以选择的选项
    default:       ContinueConversation, EndConversation
  - name:          fewShotExamples
    description:   AI学习的少量示例
    is_required:   true
  - name:          request
    description:   用户的请求
    is_required:   true
execution_settings:
  default:
    max_tokens:   10
    temperature:  0
  gpt-3.5-turbo:
    model_id:     gpt-3.5-turbo-0613
    max_tokens:   10
    temperature:  0.2
  gpt-4:
    model_id:     gpt-4-1106-preview
    max_tokens:   10
    temperature:  0.2
```

您应该注意到，现在在YAML文件中定义了与*config.json*文件中定义的相同属性。此外，`template`属性用于定义提示模板。

作为最佳实践，我们建议将您的提示添加为嵌入资源。为此，您需要更新*csproj*文件以包括以下内容：

```xml
<ItemGroup>
     <EmbeddedResource Include="Prompts\**\*.yaml" />
</ItemGroup>
```

最后，您可以在*Program.cs*文件中导入您的提示。

```csharp
// 从YAML加载提示
using StreamReader reader = new(Assembly.GetExecutingAssembly().GetManifestResourceStream("Resources." + "getIntent.prompt.yaml")!);
KernelFunction getIntent = kernel.CreateFunctionFromPromptYaml(
    await reader.ReadToEndAsync(),
    promptTemplateFactory: new HandlebarsPromptTemplateFactory()
);
```

要调用提示，您可以使用以下代码：

```csharp
// 调用handlebars提示
var intent = await kernel.InvokeAsync(
    getIntent,
    new()
    {
        { "request", userInput },
        { "choices", choices },
        { "history", history },
        { "fewShotExamples", fewShotExamples }
    }
);
```

## 迈出下一步

现在您已经知道如何保存您的提示，您可以开始学习如何创建一个代理。
