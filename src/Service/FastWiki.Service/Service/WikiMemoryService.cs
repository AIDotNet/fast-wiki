using Microsoft.KernelMemory.Configuration;
using Microsoft.KernelMemory.ContentStorage.DevTools;
using Microsoft.KernelMemory.FileSystem.DevTools;
using Microsoft.KernelMemory.Postgres;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FastWiki.Service.Service;

/// <summary>
/// 知识库内存服务
/// </summary>
public sealed class WikiMemoryService : ISingletonDependency
{
    private static readonly OpenAIHttpClientHandler HttpClientHandler = new();

    /// <summary>
    /// 创建知识库内存服务
    /// </summary>
    /// <param name="searchClientConfig"></param>
    /// <param name="maxTokensPerLine"></param>
    /// <param name="chatModel"></param>
    /// <param name="embeddingModel"></param>
    /// <returns></returns>
    public MemoryServerless CreateMemoryServerless(SearchClientConfig searchClientConfig,
        int maxTokensPerLine, string? chatModel = null, string? embeddingModel = null)
    {
        var memory = new KernelMemoryBuilder()
            .WithPostgresMemoryDb(new PostgresConfig()
            {
                ConnectionString = ConnectionStringsOptions.DefaultConnection,
                TableNamePrefix = ConnectionStringsOptions.TableNamePrefix
            })
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
                TextModel = string.IsNullOrEmpty(chatModel) ? OpenAIOption.ChatModel : chatModel
            }, null, new HttpClient(HttpClientHandler))
            .WithOpenAITextEmbeddingGeneration(new OpenAIConfig()
            {
                // 如果 EmbeddingToken 为空，则使用 ChatToken
                APIKey = string.IsNullOrEmpty(OpenAIOption.EmbeddingToken)
                    ? OpenAIOption.ChatToken
                    : OpenAIOption.EmbeddingToken,
                EmbeddingModel = string.IsNullOrEmpty(embeddingModel) ? OpenAIOption.EmbeddingModel : embeddingModel,
            }, null, false, new HttpClient(HttpClientHandler))
            .Build<MemoryServerless>();

        return memory;
    }

    public MemoryServerless CreateMemoryServerless()
    {
        return new KernelMemoryBuilder()
            .WithPostgresMemoryDb(new PostgresConfig()
            {
                ConnectionString = ConnectionStringsOptions.DefaultConnection,
                TableNamePrefix = ConnectionStringsOptions.TableNamePrefix
            })
            .WithCustomTextPartitioningOptions(new TextPartitioningOptions())
            .WithOpenAITextGeneration(new OpenAIConfig()
            {
                APIKey = OpenAIOption.ChatToken,
                TextModel = OpenAIOption.ChatModel
            }, null, new HttpClient(HttpClientHandler))
            .WithOpenAITextEmbeddingGeneration(new OpenAIConfig()
            {
                // 如果 EmbeddingToken 为空，则使用 ChatToken
                APIKey = string.IsNullOrEmpty(OpenAIOption.EmbeddingToken)
                    ? OpenAIOption.ChatToken
                    : OpenAIOption.EmbeddingToken,
                EmbeddingModel = OpenAIOption.EmbeddingModel
            }, null, false, new HttpClient(HttpClientHandler))
            .Build<MemoryServerless>();
    }

    public OpenAIChatCompletionService CreateOpenAIChatCompletionService(
        string modelId,
        string? organization = null)
    {
        return new OpenAIChatCompletionService(modelId, OpenAIOption.ChatToken, organization,
            new HttpClient(HttpClientHandler));
    }
}