using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Channels;
using FastWiki.Service.Infrastructure.KM;
using FastWiki.Service.Service;
using mem0.Core;
using Microsoft.KernelMemory.Handlers;
using Microsoft.SemanticKernel.Text;
using MemoryService = mem0.NET.Services.MemoryService;

#pragma warning disable SKEXP0050

namespace FastWiki.Service.Backgrounds;

/// <summary>
///     后台任务，用于量化
/// </summary>
public sealed class QuantizeBackgroundService : BackgroundService
{
    private const string Mem0Prompt = """
                                      推断出提供文本中的事实、偏好和记忆。
                                      只需以要点形式返回事实、偏好和记忆。:
                                      自然语言文本: {user_input}
                                      User/Agent details: {metadata}

                                      推导事实、偏好和记忆的约束：
                                      - 事实、偏好和记忆应简洁明了，但是需要含有重要信息。
                                      - 不要以“这个人喜欢披萨”开头。相反，从“喜欢披萨”开始。
                                      - 不要记住所提供的User/Agent详细信息。只记住事实、偏好和回忆。
                                      推导出的事实、偏好和记忆:

                                      """;

    /// <summary>
    ///     当前任务数量
    /// </summary>
    private static int _currentTask;

    /// <summary>
    ///     最大量化任务数量
    /// </summary>
    private static int _maxTask = 1;

    private static readonly Channel<WikiDetail> WikiDetails = Channel.CreateBounded<WikiDetail>(
        new BoundedChannelOptions(1000)
        {
            SingleReader = true,
            SingleWriter = false
        });

    private readonly ILogger<QuantizeBackgroundService> _logger;

    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     构造
    /// </summary>
    public QuantizeBackgroundService(IServiceProvider serviceProvider, ILogger<QuantizeBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    ///     执行
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // 获取环境变量中的最大任务数量
        var QUANTIZE_MAX_TASK = Environment.GetEnvironmentVariable("QUANTIZE_MAX_TASK");
        if (!string.IsNullOrEmpty(QUANTIZE_MAX_TASK)) int.TryParse(QUANTIZE_MAX_TASK, out _maxTask);

        if (_maxTask < 0) _maxTask = 1;

        // TODO: 首次启动程序的时候需要加载未处理的量化数据
        await LoadingWikiDetailAsync();

        var tasks = new List<Task>();
        for (var i = 0; i < _maxTask; i++) tasks.Add(Task.Factory.StartNew(WikiDetailHandlerAsync, stoppingToken));

        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// </summary>
    /// <param name="wikiDetail"></param>
    public static async Task AddWikiDetailAsync(WikiDetail wikiDetail)
    {
        await WikiDetails.Writer.WriteAsync(wikiDetail);
    }

    private async Task WikiDetailHandlerAsync()
    {
        using var asyncServiceScope = _serviceProvider.CreateScope();
        while (await WikiDetails.Reader.WaitToReadAsync())
        {
            Interlocked.Increment(ref _currentTask);
            _logger.LogInformation($"当前任务数量：{_currentTask}");
            var wikiDetail = await WikiDetails.Reader.ReadAsync();
            await HandlerAsync(wikiDetail, asyncServiceScope.ServiceProvider);
            Interlocked.Decrement(ref _currentTask);
        }
    }

    /// <summary>
    ///     处理量化
    /// </summary>
    /// <param name="wikiDetail"></param>
    /// <param name="service"></param>
    private async ValueTask HandlerAsync(WikiDetail wikiDetail, IServiceProvider service)
    {
        var fileStorageRepository = service.GetRequiredService<IFileStorageRepository>();
        var wikiRepository = service.GetRequiredService<IWikiRepository>();
        var wikiMemoryService = service.GetRequiredService<WikiMemoryService>();

        var wiki = await wikiRepository.FindAsync(x => x.Id == wikiDetail.WikiId);

        var id = await wikiRepository.CreateQuantizationListAsync(wiki.Id, wikiDetail.Id,
            $"创建量化任务：{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId}");
        try
        {
            if (wiki.VectorType == VectorType.Mem0)
            {
                await HandlerMem0(fileStorageRepository, wikiRepository, wikiDetail, wiki,
                    service.GetRequiredService<MemoryService>(), id);
            }
            else
            {
                if (wikiDetail.Mode == ProcessMode.Auto)
                {
                    wikiDetail.MaxTokensPerLine = 300;
                    wikiDetail.MaxTokensPerParagraph = 1000;
                    wikiDetail.OverlappingTokens = 100;
                }

                // 获取知识库配置的模型，如果没有则使用默认模型
                var serverless = wikiMemoryService.CreateMemoryServerless(new SearchClientConfig(),
                    wikiDetail.MaxTokensPerLine, wikiDetail.MaxTokensPerParagraph, wikiDetail.OverlappingTokens,
                    wiki?.Model,
                    wiki?.EmbeddingModel);
                _logger.LogInformation($"开始量化：{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId}");
                List<string> step = [];

                if (wikiDetail.TrainingPattern == TrainingPattern.QA)
                {
                    QAHandler._wikiDetail.Value = (wiki, wikiDetail);
                    var stepName = wikiDetail.Id.ToString();
                    serverless.Orchestrator.AddHandler<TextExtractionHandler>("extract_text");
                    serverless.Orchestrator.AddHandler<QAHandler>(stepName);
                    serverless.Orchestrator.AddHandler<GenerateEmbeddingsHandler>("generate_embeddings");
                    serverless.Orchestrator.AddHandler<SaveRecordsHandler>("save_memory_records");
                    step.Add("extract_text");
                    step.Add(stepName);
                    step.Add("generate_embeddings");
                    step.Add("save_memory_records");
                }

                var result = string.Empty;
                if (wikiDetail.Type == "file")
                {
                    var fileInfoQuery = await fileStorageRepository.FindAsync(x => x.Id == wikiDetail.FileId);

                    result = await serverless.ImportDocumentAsync(fileInfoQuery.FullName,
                        wikiDetail.Id.ToString(),
                        new TagCollection
                        {
                            {
                                "wikiId", wikiDetail.WikiId.ToString()
                            },
                            {
                                "fileId", wikiDetail.FileId.ToString()
                            },
                            {
                                "wikiDetailId", wikiDetail.Id.ToString()
                            }
                        }, "wiki", step.ToArray());
                }
                else if (wikiDetail.Type == "web")
                {
                    result = await serverless.ImportWebPageAsync(wikiDetail.Path,
                        wikiDetail.Id.ToString(),
                        new TagCollection
                        {
                            {
                                "wikiId", wikiDetail.WikiId.ToString()
                            },
                            {
                                "wikiDetailId", wikiDetail.Id.ToString()
                            }
                        }, "wiki", step.ToArray());
                }
                else if (wikiDetail.Type == "data")
                {
                    result = await serverless.ImportDocumentAsync(wikiDetail.Path,
                        wikiDetail.Id.ToString(),
                        new TagCollection
                        {
                            {
                                "wikiId", wikiDetail.WikiId.ToString()
                            },
                            {
                                "wikiDetailId", wikiDetail.Id.ToString()
                            }
                        }, "wiki", step.ToArray());
                }

                await wikiRepository.UpdateDetailsState(wikiDetail.Id, WikiQuantizationState.Accomplish);

                await wikiRepository.CompleteQuantizationListAsync(id,
                    $"量化成功：{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId} {result}",
                    QuantizedListState.Success);
            }
        }
        catch (Exception e)
        {
            await wikiRepository.CompleteQuantizationListAsync(id,
                $"量化失败：{wikiDetail.FileName} {e.Message}",
                QuantizedListState.Fail);

            if (wikiDetail.State != WikiQuantizationState.Fail)
                await wikiRepository.UpdateDetailsState(wikiDetail.Id, WikiQuantizationState.Fail);
        }
    }

    private async Task HandlerMem0(IFileStorageRepository fileStorageRepository, IWikiRepository wikiRepository,
        WikiDetail wikiDetail, Wiki wiki, MemoryService requiredService, long quantizedListId)
    {
        if (wikiDetail.Mode == ProcessMode.Auto)
        {
            wikiDetail.MaxTokensPerLine = 300;
            wikiDetail.MaxTokensPerParagraph = 1000;
            wikiDetail.OverlappingTokens = 100;
        }

        ApplicationContext.HistoryTrackId.Value = wikiDetail.Id.ToString();

        var result = string.Empty;
        if (wikiDetail.Type == "file")
        {
            var fileInfoQuery = await fileStorageRepository.FindAsync(x => x.Id == wikiDetail.FileId);

            var files = await File.ReadAllTextAsync(fileInfoQuery.FullName);

            var fileContents = TextChunker.SplitMarkdownParagraphs([files], wikiDetail.MaxTokensPerParagraph,
                wikiDetail.OverlappingTokens);

            foreach (var item in fileContents)
            {
                ApplicationContext.AddMemoryMetadata.Value = new Dictionary<string, string>
                {
                    { "wikiId", wikiDetail.WikiId.ToString() },
                    {
                        "user_id", wiki.Creator.ToString()
                    },
                    {
                        "run_id", wikiDetail.Id.ToString()
                    },
                    {
                        "agent_id", wiki.Id.ToString()
                    },
                    { "wikiDetailId", wikiDetail.Id.ToString() },
                    {
                        "fileIds", fileInfoQuery.Id.ToString()
                    },
                    { "metaData", item }
                };

                await requiredService.CreateMemoryAsync(new CreateMemoryInput()
                {
                    Data = item,
                    UserId = wiki.Creator.ToString(),
                    RunId = wikiDetail.Id.ToString(),
                    AgentId = wiki.Id.ToString(),
                    Prompt = Mem0Prompt.Replace("{user_input}", item)
                        .Replace("{metadata}", JsonSerializer.Serialize(new { }))
                });
            }
        }
        else if (wikiDetail.Type == "data")
        {
            var fileContents = TextChunker.SplitMarkdownParagraphs([wikiDetail.Path],
                wikiDetail.MaxTokensPerParagraph,
                wikiDetail.OverlappingTokens);

            foreach (var item in fileContents)
            {
                ApplicationContext.AddMemoryMetadata.Value = new Dictionary<string, string>
                {
                    { "wikiId", wikiDetail.WikiId.ToString() },
                    { "wikiDetailId", wikiDetail.Id.ToString() },
                    { "metaData", item }
                };

                await requiredService.CreateMemoryAsync(new CreateMemoryInput()
                {
                    Data = item,
                    UserId = wiki.Creator.ToString(),
                    RunId = wikiDetail.Id.ToString(),
                    AgentId = wiki.Id.ToString(),
                });
            }
        }

        await wikiRepository.UpdateDetailsState(wikiDetail.Id, WikiQuantizationState.Accomplish);

        await wikiRepository.CompleteQuantizationListAsync(quantizedListId,
            $"量化成功：{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId} {result}",
            QuantizedListState.Success);
    }

    private async Task LoadingWikiDetailAsync()
    {
        using var asyncServiceScope = _serviceProvider.CreateScope();

        var wikiRepository = asyncServiceScope.ServiceProvider.GetRequiredService<IWikiRepository>();
        foreach (var wikiDetail in await wikiRepository.GetFailedDetailsAsync()) await AddWikiDetailAsync(wikiDetail);
    }
}