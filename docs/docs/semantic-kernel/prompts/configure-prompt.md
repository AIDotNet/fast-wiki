---
sidebar_position: 5
---

# 配置 Prompts

在创建提示时，您可以调整控制提示行为的参数。在Semantic Kernel中，这些参数既控制AI模型运行函数的方式，又控制函数调用和[规划者](https://learn.microsoft.com/zh-cn/semantic-kernel/agents/planners/)使用的方式。

例如，您可以使用以下代码向上一篇文章中的聊天提示添加设置

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/configure-prompts?tabs=Csharp#tabpanel_1_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/configure-prompts?tabs=Csharp#tabpanel_1_python)

在C#中，您可以定义提示的以下属性：

- **名称** - 提示的名称
- **描述** - 提示的描述
- **模板格式** - 提示模板的格式（例如，`semantic-kernel`，`Handlebars`）
- **输入变量** - 提示内部使用的变量（例如，`request`）
- **执行设置** - 可用于执行提示的不同模型的设置

C#

```csharp
// Create a template for chat with settings
var chat = kernel.CreateFunctionFromPrompt(
    new PromptTemplateConfig()
    {
        Name = "Chat",
        Description = "Chat with the assistant.",
        Template = @"{{ConversationSummaryPlugin.SummarizeConversation $history}}
                    User: {{$request}}
                    Assistant: ",
        TemplateFormat = "semantic-kernel",
        InputVariables = new List<InputVariable>()
        {
            new() { Name = "history", Description = "The history of the conversation.", IsRequired = false, Default = "" },
            new() { Name = "request", Description = "The user's request.", IsRequired = true }
        },
        ExecutionSettings =
        {
            {
                "default",
                new OpenAIPromptExecutionSettings()
                {
                    MaxTokens = 1000,
                    Temperature = 0
                }
            },
            {
                "gpt-3.5-turbo", new OpenAIPromptExecutionSettings()
                {
                    ModelId = "gpt-3.5-turbo-0613",
                    MaxTokens = 4000,
                    Temperature = 0.2
                }
            },
            {
                "gpt-4",
                new OpenAIPromptExecutionSettings()
                {
                    ModelId = "gpt-4-1106-preview",
                    MaxTokens = 8000,
                    Temperature = 0.3
                }
            }
        }
    }
);
```



## 规划者使用的参数

`description`字段和`input_variables`数组被[规划者](https://learn.microsoft.com/en-us/semantic-kernel/concepts-sk/planner)用来确定如何使用函数。`description`告诉规划者函数的作用，而`input_variables`告诉规划者如何填充输入参数。

因为这些参数会影响规划者的行为，我们建议对您提供的值进行测试，以确保它们被规划者正确使用。

在编写`description`和`input_variables`时，我们建议使用以下准则：

- `description`字段应该简短而简洁，这样在规划者提示中不会消耗太多标记（但也不要太短，以至于不够描述性）。
- 考虑同一插件中其他函数的`description`，以确保它们足够独特。如果不够独特，规划者可能无法区分它们。
- 如果您在让规划者使用函数时遇到问题，请尝试添加对何时使用函数的建议或示例。



## AI模型使用的执行设置

除了为规划者提供参数外，执行设置还允许您控制函数如何由AI模型运行。以下表格描述了许多常用模型的可用设置：

展开表

| 完成参数          | 类型    | 是否必需？ | 默认值 | 描述                                                         |
| :---------------- | :------ | :--------- | :------ | :----------------------------------------------------------- |
| `max_tokens`      | 整数    | 可选       | 16      | 完成中生成的标记的最大数目。您的提示的标记数量加上max_tokens不能超过模型的上下文长度。大多数模型的上下文长度为2048个标记（除了davinci-codex，它支持4096个标记）。 |
| `temperature`     | 数字    | 可选       | 1       | 要使用的采样温度。更高的值意味着模型会冒更大的风险。尝试0.9以进行更有创意的应用，尝试0（argmax采样）以进行有明确定义答案的应用。我们通常建议修改这个或`top_p`而不是两者都修改。 |
| `top_p`           | 数字    | 可选       | 1       | 采样温度的替代方案，称为核采样，其中模型考虑具有top_p概率质量的标记的结果。因此，0.1意味着只考虑占前10%概率质量的标记。我们通常建议修改这个或温度而不是两者都修改。 |
| `presence_penalty` | 数字    | 可选       | 0       | -2.0到2.0之间的数字。正值根据新标记是否出现在迄今为止的文本中对其进行惩罚，增加模型谈论新主题的可能性。 |
| `frequency_penalty` | 数字    | 可选       | 0       | -2.0到2.0之间的数字。正值根据迄今为止文本中现有频率对新标记进行惩罚，减少模型重复相同行的可能性。 |

要了解有关OpenAI和Azure OpenAI模型的各种参数的更多信息，请访问[Azure OpenAI参考](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference)文章。

### OpenAI和Azure OpenAI的默认设置

如果您不提供完成参数，Semantic Kernel将使用OpenAI API的默认参数。通过阅读[Azure OpenAI API参考](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference)文章，了解更多有关当前默认值的信息。