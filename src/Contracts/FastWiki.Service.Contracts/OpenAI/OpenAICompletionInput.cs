namespace FastWiki.Service.Contracts.OpenAI;


public class ChatCompletionDto
{
    public string model { get; set; }

    public double? temperature { get; set; }

    /// <summary>
    /// 用温度采样的另一种方法称为核采样，其中模型考虑具有top_p概率质量的token的结果。因此0.1意味着只考虑包含前10%概率质量的标记。我们通常建议改变这个或“温度”，但不建议两者都改变。
    /// </summary>
    public double? top_p { get; set; }

    public bool stream { get; set; } = true;

    /// <summary>
    /// 生成的答案允许的最大标记数。默认情况下，模型可以返回的token数量为(4096 -提示token)。
    /// </summary>
    public double max_tokens { get; set; } = 2048;

    /// <summary>
    /// 在-2.0到2.0之间的数字。正值会根据新标记在文本中存在的频率来惩罚它们，降低模型逐字重复同一行的可能性。[有关频率和存在惩罚的更多信息。](https://docs.api-reference/parameter -details)
    /// </summary>
    public double? frequency_penalty { get; set; }

    public OpenAIError OpenAiError { get; set; }

}

public class ChatInputCompletionDto : ChatCompletionDto
{
    /// <summary>
    /// 选择应用程序的ID。如果未指定，则不适用。
    /// </summary>
    public long? applicationId { get; set; }

    /// <summary>
    /// 选择知识库的ID。如果未指定，则不适用。
    /// </summary>
    public long? userbankId { get; set; }
}

public class ChatCompletionDto<T> : ChatCompletionDto
{
    public List<T> messages { get; set; }
}

public class ChatCompletionRequestMessage
{
    public string role { get; set; }

    public string content { get; set; }

    public string? name { get; set; }
}

public class OpenAIError
{
    public string message { get; set; }
    public string type { get; set; }
    public object param { get; set; }
    public string code { get; set; }
}

public class ChatVisionCompletionRequestMessage
{
    public string role { get; set; }

    public ChatContent[] content { get; set; }

    public string? name { get; set; }
}

public class ChatContent
{
    public string type { get; set; }

    public string text { get; set; }

    public object image_url { get; set; }
}

public class ImageUrl
{
    public string detail { get; set; }
    
    public string url { get; set; }
}