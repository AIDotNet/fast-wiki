---
sidebar_position: 7
---

# Prompts模板语法

语义核心提示模板语言是一种简单的方式，使用纯文本定义和组合AI功能。您可以使用它来创建自然语言提示，生成响应，提取信息，调用其他提示或执行可以用文本表示的任何其他任务。

该语言支持三个基本特性，允许您1) 包含变量，2) 调用外部函数，3) 向函数传递参数。

您不需要编写任何代码或导入任何外部库，只需使用花括号 `{{...}}` 将表达式嵌入到您的提示中。语义核心将解析您的模板并执行其后的逻辑。这样，您可以轻松地将AI集成到您的应用程序中，几乎不费力，而且灵活性最大化。

提示

如果您需要更多功能，我们还支持[Handlebars](https://handlebarsjs.com/)模板引擎，它允许您使用循环、条件语句和其他高级功能。请参阅[此处使用Handlebars模板引擎](https://learn.microsoft.com/zh-cn/semantic-kernel/prompts/templatizing-prompts#using-the-handlebars-template-engine)。

变量

要在您的提示中包含变量值，请使用`{{$variableName}}`语法。例如，如果您有一个名为`name`的变量，保存了用户的姓名，您可以这样写：

```text
Hello {{$name}}，欢迎来到语义核心！
```

这将产生一个带有用户姓名的问候语。

空格会被忽略，所以如果您觉得这样更可读，您也可以这样写：

```text
Hello {{ $name }}，欢迎来到语义核心！
```

函数调用

要调用外部函数并在您的提示中嵌入结果，请使用`{{namespace.functionName}}`语法。例如，如果您有一个名为`weather.getForecast`的函数，返回给定位置的天气预报，您可以这样写：

```text
今天的天气是{{weather.getForecast}}。
```

这将产生一个带有存储在`input`变量中的默认位置的天气预报的句子。在调用函数时，内核会自动设置`input`变量。例如，上面的代码等同于：

```text
今天的天气是{{weather.getForecast $input}}。
```

函数参数

要调用外部函数并向其传递参数，请使用`{{namespace.functionName $varName}}`和`{{namespace.functionName "value"}}`语法。例如，如果您想将不同的输入传递给天气预报函数，您可以这样写：

```text
{{$city}}的今天天气是{{weather.getForecast $city}}。
Schio的今天天气是{{weather.getForecast "Schio"}}。
```

这将产生两个句子，使用存储在`city`变量中的城市和硬编码在提示模板中的*"Schio"*位置值的天气预报。

关于特殊字符的注释

语义函数模板是文本文件，因此无需转义特殊字符，如换行符和制表符。但是，有两种情况需要特殊语法：

1. 在提示模板中包含双花括号
2. 传递包含引号的硬编码值给函数

需要双花括号的提示

双花括号有一个特殊用例，它们用于将变量、值和函数注入模板中。

如果您需要在您的提示中包含**`{{`**和**`}}`**序列，这可能会触发特殊的渲染逻辑，最好的解决方案是使用引号括起的字符串值，如 `{{ "{{" }}`和`{{ "}}" }}`

例如：

```text
{{ "{{" }}和{{ "}}" }}是特殊的SK序列。
```

将渲染为：

```text
{{ and }}是特殊的SK序列。
```

包含引号的值和转义

值可以使用**单引号**和**双引号**括起来。

为了避免需要特殊语法，当使用包含*单引号*的值时，我们建议用*双引号*将值包起来。同样，当使用包含*双引号*的值时，用*单引号*将值包起来。

例如：

```text
...text... {{ functionName "one 'quoted' word" }} ...text...
...text... {{ functionName 'one "quoted" word' }} ...text...
```

对于那些包含单引号和双引号的值，您需要*转义*，使用特殊的**«`\`»**符号。

在使用双引号包围值时，使用**«`\"`»**来在值内包含双引号符号：

```text
... {{ "quotes' \"escaping\" example" }} ...
```

同样，在使用单引号时，使用**«`\'`»**来在值内包含单引号：

```text
... {{ 'quotes\' "escaping" example' }} ...
```

这两个都将渲染为：

```text
... quotes' "escaping" example ...
```

请注意，为了一致性，序列**«`\'`»**和**«`\"`»**总是渲染为**«`'`»**和**«`"`»**，即使可能不需要转义。

例如：

```text
... {{ 'no need to \"escape" ' }} ...
```

等同于：

```text
... {{ 'no need to "escape" ' }} ...
```

并且都会渲染为：

```text
... no need to "escape" ...
```

如果您需要在引号前面渲染一个反斜杠，因为**«`\`»**是一个特殊字符，您也需要转义它，并使用特殊的序列**«`\\\'`»**和**«`\\\"`»**。

例如：

```text
{{ 'two special chars \\\' here' }}
```

渲染为：

```text
two special chars \' here
```

与单引号和双引号一样，符号**«`\`»**在不需要转义时并不总是需要转义。但是，为了一致性，即使不需要转义，也可以转义。

例如：

```text
... {{ 'c:\\documents\\ai' }} ...
```

等同于：

```text
... {{ 'c:\documents\ai' }} ...
```

两者都渲染为：

```text
... c:\documents\ai ...
```

最后，反斜杠仅在用于**«`'`»**、**«`"`»**和**«`\`»**时具有特殊含义。

在所有其他情况下，反斜杠字符没有影响，会原样渲染。例如：

```text
{{ "nothing special about these sequences: \0 \n \t \r \foo" }}
```

渲染为：

```text
nothing special about these sequences: \0 \n \t \r \foo
```
