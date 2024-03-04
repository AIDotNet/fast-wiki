namespace FastWiki.Service.Contracts;

public class ModelConfig
{
    public ChatModel[] ChatModel { get; set; }
    public EmbeddingModel[] EmbeddingModel { get; set; }
}

public class ChatModel
{
    public string label { get; set; }
    public string value { get; set; }
}

public class EmbeddingModel
{
    public string label { get; set; }
    public string value { get; set; }
}