using FastWiki.FunctionCall;
using FastWiki.Service.Domain.Function.Aggregates;
using Microsoft.KernelMemory.Configuration;
using Microsoft.KernelMemory.ContentStorage.DevTools;
using Microsoft.KernelMemory.FileSystem.DevTools;
using Microsoft.KernelMemory.Postgres;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace FastWiki.Service.Service;

/// <summary>
/// 知识库内存服务
/// </summary>
public sealed class WikiMemoryService : ISingletonDependency
{
    private static FastWikiFunctionContext _context = new();

    private static readonly OpenAiHttpClientHandler HttpClientHandler = new();

    /// <summary>
    /// 创建知识库内存服务
    /// </summary>
    /// <param name="searchClientConfig"></param>
    /// <param name="maxTokensPerLine"></param>
    /// <param name="maxTokensPerParagraph"></param>
    /// <param name="overlappingTokens"></param>
    /// <param name="chatModel"></param>
    /// <param name="embeddingModel"></param>
    /// <returns></returns>
    public MemoryServerless CreateMemoryServerless(SearchClientConfig searchClientConfig,
        int maxTokensPerLine,
        int maxTokensPerParagraph,
        int overlappingTokens,
        string? chatModel = null, string? embeddingModel = null)
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
                MaxTokensPerParagraph = maxTokensPerParagraph,
                OverlappingTokens = overlappingTokens
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

    public Kernel CreateFunctionKernel(List<FastWikiFunctionCall> fastWikiFunctionCalls, string apiKey, string modelId,
        string uri)
    {
        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: modelId,
                apiKey: apiKey,
                httpClient: new HttpClient(new OpenAiHttpClientHandler(uri)))
            .Build();

        foreach (var fastWikiFunctionCall in fastWikiFunctionCalls)
        {
            var function = kernel.CreateFunctionFromMethod(async (dynamic value) =>
                {
                    var result = await _context.FunctionCall(fastWikiFunctionCall.Content, fastWikiFunctionCall.Main,
                        value);

                    return result;
                },
                fastWikiFunctionCall.Main,
                fastWikiFunctionCall.Description,
                fastWikiFunctionCall.Parameters.Select(x => new KernelParameterMetadata(x.Key)
                {
                    Description = x.Value,
                    Name = x.Key
                }));

            kernel.Plugins.AddFromFunctions(fastWikiFunctionCall.Main, fastWikiFunctionCall.Description,
                new[] { function });
        }

        return kernel;
    }

    public Kernel CreateFunctionKernel(string apiKey, string modelId, string uri)
    {
        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: modelId,
                apiKey: apiKey,
                httpClient: new HttpClient(new OpenAiHttpClientHandler(uri)))
            .Build();

        return kernel;
    }
}