---
sidebar_position: 6
---

# 对Semantic Kernel的贡献

您可以通过提交问题、开始讨论和提交拉取请求（PR）来为Semantic Kernel做出贡献。非常感谢您的代码贡献，但仅仅提交遇到的问题的问题也是一种很好的贡献方式，因为这有助于我们集中精力解决问题。

## 报告问题和反馈

我们始终欢迎错误报告、API提案和总体反馈。由于我们使用GitHub，您可以使用[问题](https://github.com/microsoft/semantic-kernel/issues)和[讨论](https://github.com/microsoft/semantic-kernel/discussions)标签与团队开始对话。在提交问题和反馈时，请参考以下几点，以便我们尽快回应您的反馈。

### 报告问题

SDK的新问题可以在我们的[问题列表](https://github.com/microsoft/semantic-kernel/issues)中报告，但在提交新问题之前，请搜索问题列表，以确保问题不存在。如果您在Semantic Kernel文档（本站点）中遇到问题，请在[Semantic Kernel文档存储库](https://github.com/MicrosoftDocs/semantic-kernel-docs/issues)中报告问题。

如果您*确实*发现了您想要报告的现有问题，请在讨论中包含您自己的反馈。我们还强烈建议对原始帖子进行点赞（👍反应），因为这有助于我们在待办事项中优先处理受欢迎的问题。

#### 编写良好的错误报告

良好的错误报告可以帮助维护者验证和找出潜在问题的根本原因。错误报告越好，问题就可以越快解决。理想情况下，错误报告应包含以下信息：

- 问题的高级描述。
- *最小重现*，即重现错误行为所需的最小代码/配置大小。
- *预期行为*的描述，与观察到的*实际行为*进行对比。
- 环境信息：操作系统/发行版、CPU架构、SDK版本等。
- 其他信息，例如：是否是从以前的版本中产生的回归？是否有任何已知的解决方法？

[创建问题](https://github.com/microsoft/semantic-kernel/issues)

### 提交反馈

如果您对Semantic Kernel有一般性反馈或者对如何改进它有想法，请在我们的[讨论板](https://github.com/microsoft/semantic-kernel/discussions)上分享。在开始新讨论之前，请搜索讨论列表，以确保其不存在。

如果您有特定想法要分享，请使用[想法类别](https://github.com/microsoft/semantic-kernel/discussions/categories/ideas)，如果您对Semantic Kernel有问题，请使用[问答类别](https://github.com/microsoft/semantic-kernel/discussions/categories/q-a)。

您也可以加入[Semantic KernelDiscord服务器](https://aka.ms/sk/discord)在Discord社区开始讨论（并分享您创建的任何反馈）。

[开始讨论](https://github.com/microsoft/semantic-kernel/discussions)

### 帮助我们优先处理反馈

我们目前使用点赞来帮助我们在待办事项中优先处理问题和功能，因此请点赞您希望解决的任何问题或讨论。

如果您认为其他人将受益于某个功能，我们还鼓励您要求其他人点赞该问题。这有助于我们优先处理影响最多用户的问题。您可以要求同事、朋友或[Discord社区](https://aka.ms/sk/discord)上的其他人通过分享问题或讨论的链接来对问题进行点赞。

## 提交拉取请求

我们欢迎对Semantic Kernel的贡献。如果您有要贡献的错误修复或新功能，请按照以下步骤提交拉取请求（PR）。之后，项目维护人员将审查代码更改，并在获得接受后将其合并。

### 推荐的贡献工作流程

我们建议使用以下工作流程来为Semantic Kernel做出贡献（这是Semantic Kernel团队使用的相同工作流程）：

1. 为您的工作创建一个问题。

   - 对于微不足道的更改，您可以跳过此步骤。
   - 如果有相关主题的现有问题，请重复使用。
   - 通过在问题的讨论中获得团队和社区对您提议的更改是否合适的同意，明确说明您将实施。这可以让我们将问题分配给您，并确保其他人不会意外地开始处理它。

2. 在GitHub上创建存储库的个人分支（如果您还没有）。

3. 在您的分支中，从main分支创建一个分支（

   ```shell
   git checkout -b mybranch
   ```

   ）。

   - 以清晰表达您意图的方式命名分支，例如“issue-123”或“githubhandle-issue”。

4. 对您的分支进行更改并提交。

5. 如果适用，为您的更改添加新的测试。

6. 使用您的更改构建存储库。

   - 确保构建是干净的。
   - 确保所有测试都通过，包括您的新测试。

7. 对存储库的

   main

   分支创建PR。

   - 在描述中说明您的更改是解决的问题或改进。
   - 验证所有持续集成检查是否通过。

8. 等待代码维护人员的反馈或批准您的更改。

9. 当区域所有者签署并且所有检查都通过时，您的PR将被合并。

### 贡献时的Dos and Don'ts

以下是我们在贡献给Semantic Kernel时推荐的Dos和Don'ts，以帮助我们尽快审查和合并您的更改。

#### Do's:

- **要** 遵循标准的[.NET编码风格](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)和[Python代码风格](https://pypi.org/project/black/)
- **要** 优先考虑项目或文件当前的风格，如果它与一般准则不符。
- **要** 在添加新功能时包含测试。在修复错误时，首先添加突出当前行为有问题的测试。
- **要** 保持讨论的专注。当出现新的或相关主题时，创建新问题通常比侧重讨论更好。
- **要** 明确说明您将实施问题。
- **要** 关于您的贡献进行博客和/或推文！

#### Don'ts:

- **不要** 用大型拉取请求让团队感到意外。我们希望支持贡献者，因此我们建议先提交问题并开始讨论，以便在投入大量时间之前我们可以就方向达成一致。
- **不要** 提交您没有编写的代码。如果您发现代码适合添加到Semantic Kernel，请先提交问题并开始讨论。
- **不要** 提交更改许可相关文件或标题的PR。如果您认为存在问题，请提交问题，我们将乐意讨论。
- **不要** 在未提交问题并与团队讨论的情况下创建新API。向库添加新的公共表面积是一件大事，我们希望确保我们做得正确。

### 破坏性更改

贡献必须保持API签名和行为兼容。如果您想要进行会破坏现有代码的更改，请提交一个问题来讨论您的想法或更改，如果您认为需要进行重大更改。否则，包含重大更改的贡献将被拒绝。

### 持续集成（CI）流程

持续集成（CI）系统将自动执行所需的构建并对PR运行测试（包括您在本地也应该运行的测试）。在PR合并之前，构建和测试运行必须是干净的。

如果CI构建由于任何原因失败，PR问题将被更新，包含一个可以用来确定失败原因的链接，以便解决问题。

### 贡献文档

我们也接受对[Semantic Kernel文档存储库](https://github.com/MicrosoftDocs/semantic-kernel-docs/issues)的贡献。要了解如何进行贡献，请从微软[文档贡献者指南](https://learn.microsoft.com/en-us/contribute)开始。
