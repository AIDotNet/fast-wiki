namespace FastWiki.Service.Input;

public class CreateChatApplicationInput
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 对话模型
    /// </summary>
    public string ChatModel { get; private set; }

    /// <summary>
    /// 温度
    /// </summary>
    public double Temperature { get; set; }
}