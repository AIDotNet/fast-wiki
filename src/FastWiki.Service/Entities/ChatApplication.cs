using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;

namespace FastWiki.Service.Entities;

public sealed class ChatApplication : FullAggregateRoot<string, Guid?>
{
    protected ChatApplication()
    {
    }

    public ChatApplication(string name, string chatModel, double temperature)
    {
        Name = name;
        ChatModel = chatModel;
        Temperature = temperature;
    }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 提示词
    /// </summary>
    public string Prompt { get; private set; }

    /// <summary>
    /// 对话模型
    /// </summary>
    public string ChatModel { get; private set; }

    /// <summary>
    /// 温度
    /// </summary>
    public double Temperature { get; set; }

    /// <summary>
    /// 最大响应Token数量
    /// </summary>
    public int MaxResponseToken { get; set; }

    /// <summary>
    /// 模板
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// 参数
    /// </summary>
    public Dictionary<string, string> Parameter { get; set; }

    /// <summary>
    /// 未找到的回答模板
    /// </summary>
    public string Opener { get; set; }

    /// <summary>
    /// 关联的知识库
    /// </summary>
    public List<long> WikiIds { get; set; }

    /// <summary>
    /// 引用上限
    /// </summary>
    public int ReferenceUpperLimit { get; set; }

    /// <summary>
    /// 匹配相似度
    /// </summary>
    public double Relevancy { get; set; }

    /// <summary>
    /// 未找到的回答模板
    /// 如果模板为空则使用Chat对话模型回答。
    /// </summary>
    public string? NoReplyFoundTemplate { get; set; }

    /// <summary>
    /// 显示引用文件
    /// </summary>
    public bool ShowSourceFile { get; set; }

    /// <summary>
    /// 扩展字段
    /// </summary>
    public Dictionary<string, string> Extend { get; set; }

    /// <summary>
    /// 关联function
    /// </summary>
    public List<long> FunctionIds { get; set; }

    public void SetPrompt(string prompt)
    {
        Prompt = prompt;
    }

    public void SetOpener(string opener)
    {
        Opener = opener;
    }

    public void SetNoReplyFoundTemplate(string noReplyFoundTemplate)
    {
        NoReplyFoundTemplate = noReplyFoundTemplate;
    }

    public void SetExtend(Dictionary<string, string> extend)
    {
        Extend = extend;
    }

    public void SetWikiIds(List<long> wikiIds)
    {
        WikiIds = wikiIds;
    }

    public void SetFunctionIds(List<long> functionIds)
    {
        FunctionIds = functionIds;
    }

    public void SetParameter(Dictionary<string, string> parameter)
    {
        Parameter = parameter;
    }

    public void SetTemplate(string template)
    {
        Template = template;
    }

    public void SetReferenceUpperLimit(int referenceUpperLimit)
    {
        ReferenceUpperLimit = referenceUpperLimit;
    }

    public void SetRelevancy(double relevancy)
    {
        Relevancy = relevancy;
    }

    public void SetMaxResponseToken(int maxResponseToken)
    {
        MaxResponseToken = maxResponseToken;
    }

    public void SetShowSourceFile(bool showSourceFile)
    {
        ShowSourceFile = showSourceFile;
    }

    public void SetTemperature(double temperature)
    {
        Temperature = temperature;
    }

    public void SetChatModel(string chatModel)
    {
        ChatModel = chatModel;
    }

    public void SetName(string name)
    {
        Name = name;
    }
}