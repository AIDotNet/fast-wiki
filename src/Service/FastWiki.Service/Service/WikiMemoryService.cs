using Microsoft.KernelMemory.Configuration;
using Microsoft.KernelMemory.ContentStorage.DevTools;
using Microsoft.KernelMemory.FileSystem.DevTools;
using Microsoft.KernelMemory.Postgres;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FastWiki.Service.Service;

/// <summary>
/// 知识库内存服务
/// </summary>
/// <param name="configuration"></param>
public class WikiMemoryService(IConfiguration configuration) : ISingletonDependency
{
    private static readonly OpenAIHttpClientHandler HttpClientHandler = new();
    
    public MemoryServerless CreateMemoryServerless(SearchClientConfig searchClientConfig,
        int maxTokensPerLine)
    {
        var memory = new KernelMemoryBuilder()
            .WithPostgresMemoryDb(configuration.GetConnectionString("DefaultConnection"))
            .WithSimpleFileStorage(new SimpleFileStorageConfig
            {
                StorageType = FileSystemTypes.Volatile,
                Directory = "_files"
            })
            .WithSearchClientConfig(searchClientConfig)
            .WithCustomTextPartitioningOptions(new TextPartitioningOptions()
            {
                MaxTokensPerLine = maxTokensPerLine,
            })
            .WithOpenAITextGeneration(new OpenAIConfig()
            {
                APIKey = OpenAIOption.ChatToken,
                TextModel = OpenAIOption.ChatModel
            }, null, new HttpClient(HttpClientHandler))
            .WithOpenAITextEmbeddingGeneration(new OpenAIConfig()
            {
                APIKey = OpenAIOption.ChatToken,
                EmbeddingModel = OpenAIOption.EmbeddingModel,
            }, null, false, new HttpClient(HttpClientHandler))
            .Build<MemoryServerless>();

        return memory;
    }

    public OpenAIChatCompletionService CreateOpenAIChatCompletionService(
        string modelId,
        string? organization = null)
    {
        return new OpenAIChatCompletionService(modelId, OpenAIOption.ChatToken, organization, new HttpClient(HttpClientHandler));
    }
}