namespace FastWiki.Web.Rcl.Helper;

public class ChatHelper
{
    private static ModelConfig _modelConfig;

    static ChatHelper()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "model.json");
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            _modelConfig = JsonSerializer.Deserialize<ModelConfig>(json);
        }
        else
        {
            // 不存在则创建默认配置
            _modelConfig = new ModelConfig
            {
                ChatModel = new List<(string, string)>
                {
                    new("gpt-3.5-turbo", "gpt-3.5-turbo"),
                    new("gpt-4-0125-preview", "gpt-4-0125-preview"),
                    new("gpt-4-1106-preview", "gpt-4-1106-preview"),
                    new("gpt-4-1106-vision-preview", "gpt-4-1106-vision-preview"),
                    new("gpt-4", "gpt-4"),
                    new("gpt-4-32k", "gpt-4-32k"),
                    new("gpt-3.5-turbo-0125", "gpt-3.5-turbo-0125")
                }.Select(x => new ChatModel { label = x.Item1, value = x.Item2 }).ToArray(),
                EmbeddingModel = new List<(string, string)>
                    {
                        new("text-embedding-3-small", "text-embedding-3-small")
                    }
                    .Select(x => new EmbeddingModel { label = x.Item1, value = x.Item2 }).ToArray()
            };
            var json = JsonSerializer.Serialize(_modelConfig);
            File.WriteAllText(path, json);
        }
    }

    public static List<(string, string)> GetChatModel()
    {
        return _modelConfig.ChatModel.Select(x => (x.label, x.value)).ToList();
    }

    public static List<(string, string)> GetEmbeddingModel()
    {
        return _modelConfig.EmbeddingModel.Select(x => (x.label, x.value)).ToList();
    }
}