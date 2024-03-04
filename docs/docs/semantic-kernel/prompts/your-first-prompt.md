---
sidebar_position: 2
---

# Semantic Kernel Prompts AI 模型

Prompts是从AI模型中获得正确结果的核心。在本文中，我们将演示如何在使用Semantic Kernel时使用常见的Prompts工程技术。

如果您想查看本教程的最终解决方案，可以在公共文档存储库中查看以下示例。

| 语言     | 链接到最终解决方案                                       |
| :------- | :----------------------------------------------------------- |
| C#       | [在GitHub中打开示例](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/DocumentationExamples/Prompts.cs) |
| Python   | [在GitHub中打开解决方案](https://github.com/MicrosoftDocs/semantic-kernel-docs/tree/main/samples/python/03-Intro-to-Prompts) |

## 创建一个可以检测用户意图的Prompts

如果您曾经使用过ChatGPT或Microsoft Copilot，您已经熟悉Prompts。在给定请求的情况下，LLM将尝试预测最可能的响应。例如，如果您发送Prompts`"我想去"`，一个AI服务可能会返回`"海滩"`来完成句子。这是一个非常简单的例子，但它演示了文本生成Prompts的基本思想。

使用Semantic Kernel SDK，您可以轻松地从自己的应用程序中运行Prompts。这使您能够在自己的应用程序中利用AI模型的强大功能。

一个常见的场景是检测用户的意图，因此您可以在之后运行一些自动化，因此，在本文中，我们将展示如何创建一个可以检测用户意图的Prompts。另外，我们将演示如何通过使用Prompts工程技术逐步改进Prompts。

 Prompts

本文中的许多建议都是基于[Prompts工程指南](https://www.promptingguide.ai/introduction/basics)。如果您想成为一个编写Prompts的专家，我们强烈建议阅读并利用他们的Prompts工程技术。

## 使用Semantic Kernel运行您的第一个Prompts

如果我们想要让AI检测用户输入的意图，我们可以简单地*询问*意图是什么。在Semantic Kernel中，我们可以使用以下代码创建一个刚好可以做到这一点的字符串：

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_1_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_1_python)

C#

```csharp
Write("您的请求: ");
string request = ReadLine()!;
string prompt = $"这个请求的意图是什么？ {request}";
```

要运行此Prompts，我们现在需要创建一个带有AI服务的内核。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_2_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_2_python)

C#

```csharp
Kernel kernel = Kernel.CreateBuilder()
                      .AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey)
                      .Build();
```

最后，我们可以使用我们的新内核调用我们的Prompts。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_3_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_3_python)

C#

```csharp
WriteLine(await kernel.InvokePromptAsync(prompt));
```

如果我们使用输入"I want to send an email to the marketing team celebrating their recent milestone."运行此代码，我们应该会得到以下输出：

```
这个请求的意图是寻求指导或澄清如何有效地给市场团队发送电子邮件以庆祝他们最近的里程碑。
```

## 使用Prompts工程改进Prompts

虽然这个Prompts"有效"，但它并不是非常可用，因为您无法使用结果可预测地触发自动化。每次运行Prompts时，您可能会得到一个非常不同的响应。

为了使结果更可预测，我们可以进行以下改进：

1. [使Prompts更具体](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#1-make-the-prompt-more-specific)
2. [使用格式化为输出添加结构](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#2-add-structure-to-the-output-with-formatting)
3. [提供少量示例进行Prompts](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#3-provide-examples-with-few-shot-prompting)
4. [告诉AI应该做什么以避免做错事](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#4-tell-the-ai-what-to-do-to-avoid-doing-something-wrong)
5. [为AI提供上下文](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#5-provide-context-to-the-ai)
6. [在聊天完成Prompts中使用消息角色](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#6-using-message-roles-in-chat-completion-prompts)
7. [给AI鼓励的话](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#7-give-your-ai-words-of-encouragement)

### 1) 使Prompts更具体

我们可以做的第一件事是使我们的Prompts更具体。与其只是询问"这个请求的意图是什么？"，我们可以向AI提供一个意图列表以供选择。这将使Prompts更可预测，因为AI只能从我们提供的意图列表中选择。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_4_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_4_python)

C#

```csharp
prompt = @$"这个请求的意图是什么？ {request}
您可以在发送电子邮件、发送消息、完成任务、创建文档之间进行选择。";
```

现在，当您使用相同的输入运行Prompts时，您应该会得到一个更可用的结果，但它仍然不完美，因为AI会以附加信息回应。


```
请求的意图是发送电子邮件。因此，适当的操作应该使用发送电子邮件功能。
```

### 2) 使用格式化为输出添加结构

虽然结果更可预测，但LLM有可能以一种您无法轻松解析结果的方式回应。例如，如果LLM回应说"意图是发送电子邮件"，您可能很难提取意图，因为它不在可预测的位置上。

为了使结果更可预测，我们可以通过使用格式化为Prompts添加结构。在这种情况下，我们可以这样定义我们Prompts的不同部分：

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_5_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_5_python)

C#

```csharp
prompt = @$"说明：这个请求的意图是什么？
选择：发送电子邮件、发送消息、完成任务、创建文档。
用户输入：{request}
意图：";
```

通过使用这种格式化，AI不太可能回应一个除了意图以外的结果。

在其他提示中，您可能还想尝试使用Markdown、XML、JSON、YAML或其他格式来为您的提示和输出添加结构。由于LLMs倾向于生成看起来像提示的文本，建议您对提示和输出使用相同的格式。

例如，如果您希望LLM生成一个JSON对象，您可以使用以下提示：

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_6_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_6_python)

C#

```csharp
    prompt = @$"## Instructions
Provide the intent of the request using the following format:

```json
{{
    ""intent"": {{intent}}
}}```

## Choices
You can choose between the following intents:

```json
[""SendEmail"", ""SendMessage"", ""CompleteTask"", ""CreateDocument""]```

## User Input
The user input is:

```json
{{
    ""request"": ""{request}""
}}```

## Intent";

```

这将导致以下输出：

JSON

```json
{
    "intent": "SendEmail"
}
```

### 3) 使用few-shot提示提供示例

到目前为止，我们一直在使用零-shot提示，这意味着我们没有为AI提供任何示例。虽然这对于入门来说没问题，但对于更复杂的情况而言，这并不推荐，因为AI可能没有足够的训练数据来生成正确的结果。

要添加示例，我们可以使用few-shot提示。使用few-shot提示时，我们向AI提供我们想要它执行的一些示例。例如，我们可以提供以下示例来帮助AI区分发送电子邮件和发送即时消息。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_7_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_7_python)

C#

```csharp
        prompt = @$"说明：此请求的意图是什么？
选择：SendEmail、SendMessage、CompleteTask、CreateDocument。

用户输入：您能快速向营销团队发送批准吗？
意图：SendMessage

用户输入：您能向营销团队发送完整的更新吗？
意图：SendEmail

用户输入：{request}
意图：";
```

### 4) 告诉AI如何避免出错

通常当AI开始错误地回应时，人们很容易告诉AI停止做某事。不幸的是，这往往会导致AI做更糟糕的事情。例如，如果您告诉AI停止返回虚构的意图，它可能开始返回与用户请求完全无关的意图。

相反，建议您告诉AI应该*做什么*。例如，如果您想告诉AI停止返回虚构的意图，您可以编写以下提示。

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_8_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_8_python)

C#

```csharp
        prompt = @$"说明：此请求的意图是什么？
如果您不知道意图，请不要猜测；而是回复“未知”。
选择：SendEmail、SendMessage、CompleteTask、CreateDocument、Unknown。

用户输入：您能快速向营销团队发送批准吗？
意图：SendMessage

用户输入：您能向营销团队发送完整的更新吗？
意图：SendEmail

用户输入：{request}
意图：";
```



### 5) 为AI提供上下文

在某些情况下，您可能希望为AI提供上下文，以便它更好地理解用户的请求。这对于长时间运行的聊天场景特别重要，因为用户的意图可能需要来自先前消息的上下文信息。

例如，考虑以下对话：

```
用户：我讨厌发送电子邮件，没有人会读的。
AI：很抱歉听到这个。信息可能是更好的沟通方式。
用户：我同意，你能以这种方式向营销团队发送完整的状态更新吗？
```

如果AI只收到最后一条消息，它可能会错误地回复“SendEmail”而不是“SendMessage”。但是，如果AI收到整个对话，它可能能够理解用户的意图。

为了提供这种上下文，我们只需将先前的消息添加到提示中。例如，我们可以更新我们的提示如下所示：

- [C#](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_9_Csharp)
- [Python](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/your-first-prompt?tabs=Csharp#tabpanel_9_python)

C#

```csharp
        string history = @"用户输入：我讨厌发送电子邮件，没有人会读的。
AI回复：很抱歉听到这个。信息可能是更好的沟通方式。";

        prompt = @$"说明：此请求的意图是什么？
如果您不知道意图，请不要猜测；而是回复“未知”。
选择：SendEmail、SendMessage、CompleteTask、CreateDocument、Unknown。

用户输入：您能快速向营销团队发送批准吗？
意图：SendMessage

用户输入：您能向营销团队发送完整的更新吗？
意图：SendEmail

{history}
用户输入：{request}
意图：";
```



### 6) 在聊天完成提示中使用消息角色

随着提示变得更加复杂，您可能希望使用消息角色来帮助AI区分系统指令、用户输入和AI回复。在我们开始将聊天历史添加到提示中时，这一点尤为重要。AI应该知道一些先前的消息是由它自己而不是用户发送的。

在Semantic Kernel中，使用特殊语法来定义消息角色。要定义消息角色，只需使用带有角色名称属性的`<message>`标记将消息包装起来。目前，这仅在C# SDK中可用。

C#

```csharp
        history = @"<message role=""user"">我讨厌发送电子邮件，没有人会读的。</message>
<message role=""assistant"">很抱歉听到这个。信息可能是更好的沟通方式。</message>";

        prompt = @$"<message role=""system"">说明：此请求的意图是什么？
如果您不知道意图，请不要猜测；而是回复“未知”。
选择：SendEmail、SendMessage、CompleteTask、CreateDocument、Unknown。</message>

<message role=""user"">您能快速向营销团队发送批准吗？</message>
<message role=""system"">意图：</message>
<message role=""assistant"">SendMessage</message>

<message role=""user"">您能向营销团队发送完整的更新吗？</message>
<message role=""system"">意图：</message>
<message role=""assistant"">SendEmail</message>

{history}
<message role=""user"">{request}</message>
<message role=""system"">意图：</message>";
```

### 7) 给你的AI鼓励的话

最后，研究表明，给你的AI鼓励的话可以帮助它表现更好。例如，为了好的结果提供奖励或者奖励可以产生更好的结果。

C#

```csharp
        history = @"<message role=""user"">我讨厌发邮件，没人会读的。</message>
<message role=""assistant"">很抱歉听到这个。消息可能是更好的沟通方式。</message>";

        prompt = @$"<message role=""system"">说明：这个请求的意图是什么？
如果你不知道意图，不要猜测；而是回答""未知""。
选择：SendEmail, SendMessage, CompleteTask, CreateDocument, Unknown.
奖励：如果你回答正确，你将得到$20。</message>

<message role=""user"">你能给营销团队发送一个非常快速的批准吗？</message>
<message role=""system"">意图：</message>
<message role=""assistant"">SendMessage</message>

<message role=""user"">你能给营销团队发送完整的更新吗？</message>
<message role=""system"">意图：</message>
<message role=""assistant"">SendEmail</message>

{history}
<message role=""user"">{request}</message>
<message role=""system"">意图：</message>";
```

## 下一步

现在你知道如何编写提示了，你可以学习如何将其模板化，使其更加灵活和强大。