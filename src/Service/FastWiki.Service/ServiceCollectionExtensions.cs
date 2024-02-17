using FastWiki.Service;
using Microsoft.KernelMemory;
using Microsoft.KernelMemory.ContentStorage.DevTools;
using Microsoft.KernelMemory.FileSystem.DevTools;
using Microsoft.KernelMemory.Postgres;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注入FastSemanticKernel
    /// </summary>
    /// <param name="builder"></param>
    public static void AddFastSemanticKernel(this WebApplicationBuilder builder)
    {
        var handler = new OpenAIHttpClientHandler();
        
        builder.Services.AddScoped<Kernel>(_ =>
        {
            var kernel = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(
                    modelId: OpenAIOption.ChatModel,
                    apiKey: OpenAIOption.ChatToken,
                    httpClient: new HttpClient(handler))
                .Build();
#pragma warning disable SKEXP0050 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            kernel.ImportPluginFromObject(new ConversationSummaryPlugin(), "ConversationSummaryPlugin");
#pragma warning restore SKEXP0050 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
#pragma warning disable SKEXP0050 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            kernel.ImportPluginFromObject(new TimePlugin(), "TimePlugin");
#pragma warning restore SKEXP0050 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            return kernel;
        });
        
        //Kernel Memory
        var searchClientConfig = new SearchClientConfig
        {
            MaxAskPromptSize = 128000,
            MaxMatchesCount = 3,
            AnswerTokens = 2000,
            EmptyAnswer = "知识库未搜索到相关内容"
        };
        
        builder.Services.AddScoped<MemoryServerless>(_ =>
        {
            var memory = new KernelMemoryBuilder()
                .WithPostgresMemoryDb(builder.Configuration.GetConnectionString("DefaultConnection"))
                .WithSimpleFileStorage(new SimpleFileStorageConfig { StorageType = FileSystemTypes.Volatile, Directory = "_files" })
                .WithSearchClientConfig(searchClientConfig)
                .WithOpenAITextGeneration(new OpenAIConfig()
                {
                    APIKey = OpenAIOption.ChatToken,
                    TextModel = OpenAIOption.ChatModel
                }, null, new HttpClient(handler))
                .WithOpenAITextEmbeddingGeneration(new OpenAIConfig()
                {
                    APIKey = OpenAIOption.ChatToken,
                    EmbeddingModel = OpenAIOption.EmbeddingModel,
                }, null, false, new HttpClient(handler))
                .Build<MemoryServerless>();
            return memory;
        });
    }
}