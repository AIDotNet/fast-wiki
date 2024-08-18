using FastWiki.FunctionCall;
using FastWiki.Service.Domain.Function.Aggregates;
using Microsoft.KernelMemory.Configuration;
using Microsoft.SemanticKernel;

namespace FastWiki.Service.Service;

/// <summary>
///     知识库内存服务
/// </summary>
public sealed class WikiMemoryService : ISingletonDependency
{
    private static readonly FastWikiFunctionContext Context = new();

    private static readonly OpenAiHttpClientHandler HttpClientHandler = new(OpenAIOption.ChatEndpoint);

    /// <summary>
    ///     创建知识库内存服务
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
        string? chatModel, string? embeddingModel)
    {
        return new KernelMemoryBuilder()
            .WithQdrantMemoryDb(new QdrantConfig()
            {
                APIKey = ConnectionStringsOptions.WikiAPIKey,
                Endpoint = ConnectionStringsOptions.WikiConnection
            })
            .WithSearchClientConfig(searchClientConfig)
            .WithCustomTextPartitioningOptions(new TextPartitioningOptions
            {
                MaxTokensPerLine = maxTokensPerLine,
                MaxTokensPerParagraph = maxTokensPerParagraph,
                OverlappingTokens = overlappingTokens
            })
            .WithOpenAITextGeneration(new OpenAIConfig
            {
                APIKey = OpenAIOption.ChatToken,
                TextModel = chatModel
            }, null, new HttpClient(HttpClientHandler))
            .WithOpenAITextEmbeddingGeneration(new OpenAIConfig
            {
                // 如果 EmbeddingToken 为空，则使用 ChatToken
                APIKey = string.IsNullOrEmpty(OpenAIOption.EmbeddingToken)
                    ? OpenAIOption.ChatToken
                    : OpenAIOption.EmbeddingToken,
                EmbeddingModel = embeddingModel
            }, null, false, new HttpClient(HttpClientHandler))
            .AddSingleton(new WikiMemoryService())
            .Build<MemoryServerless>();
    }

    /// <summary>
    ///     创建用于操作的内存服务（不要用于向量搜索）
    /// </summary>
    /// <returns></returns>
    public MemoryServerless CreateMemoryServerless(string embeddingModel, string? model)
    {
        return new KernelMemoryBuilder()
            .WithQdrantMemoryDb(new QdrantConfig()
            {
                APIKey = ConnectionStringsOptions.WikiAPIKey,
                Endpoint = ConnectionStringsOptions.WikiConnection
            })
            .WithOpenAITextGeneration(new OpenAIConfig
            {
                APIKey = OpenAIOption.ChatToken,
                TextModel = model
            }, null, new HttpClient(HttpClientHandler))
            .WithOpenAITextEmbeddingGeneration(new OpenAIConfig
            {
                // 如果 EmbeddingToken 为空，则使用 ChatToken
                APIKey = string.IsNullOrEmpty(OpenAIOption.EmbeddingToken)
                    ? OpenAIOption.ChatToken
                    : OpenAIOption.EmbeddingToken,
                EmbeddingModel = embeddingModel
            }, null, false, new HttpClient(HttpClientHandler))
            .Build<MemoryServerless>();
    }

    /// <summary>
    ///     创建Function Kernel
    /// </summary>
    /// <param name="fastWikiFunctionCalls"></param>
    /// <param name="chatModel"></param>
    /// <returns></returns>
    public Kernel CreateFunctionKernel(List<FastWikiFunctionCall>? fastWikiFunctionCalls,
        string chatModel)
    {
        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                chatModel,
                OpenAIOption.ChatToken,
                httpClient: new HttpClient(new OpenAiHttpClientHandler(OpenAIOption.ChatEndpoint)
                {
                    // 设置请求序列化编码
                }))
            .Build();

        if (fastWikiFunctionCalls != null)
            foreach (var fastWikiFunctionCall in fastWikiFunctionCalls)
            {
                var function = kernel.CreateFunctionFromMethod(async (dynamic value) =>
                    {
                        var result = await Context.FunctionCall(fastWikiFunctionCall.Content, fastWikiFunctionCall.Main,
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

    /// <summary>
    ///     创建Function Kernel
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="modelId"></param>
    /// <param name="uri"></param>
    /// <returns></returns>
    public Kernel CreateFunctionKernel(string apiKey, string modelId, string uri)
    {
        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId,
                apiKey,
                httpClient: new HttpClient(new OpenAiHttpClientHandler(uri)))
            .Build();

        return kernel;
    }
}