using System.Collections.Concurrent;
using System.Threading.Channels;
using FastWiki.Service.Infrastructure.KM;
using FastWiki.Service.Service;
using Microsoft.KernelMemory.Handlers;

namespace FastWiki.Service.Backgrounds;

/// <summary>
/// 后台任务，用于量化
/// </summary>
public sealed class QuantizeBackgroundService : BackgroundService
{
    /// <summary>
    /// 线程安全字典
    /// </summary>
    public static ConcurrentDictionary<string, (WikiDetail, Wiki)> CacheWikiDetails { get; } = new();

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<QuantizeBackgroundService> _logger;

    /// <summary>
    /// 当前任务数量
    /// </summary>
    private static int _currentTask;

    /// <summary>
    /// 最大量化任务数量
    /// </summary>
    private static int _maxTask = 1;

    private static readonly Channel<WikiDetail> WikiDetails = Channel.CreateBounded<WikiDetail>(
        new BoundedChannelOptions(1000)
        {
            SingleReader = true,
            SingleWriter = false
        });

    /// <summary>
    /// 构造
    /// </summary>
    public QuantizeBackgroundService(IServiceProvider serviceProvider, ILogger<QuantizeBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // 获取环境变量中的最大任务数量
        var QUANTIZE_MAX_TASK = Environment.GetEnvironmentVariable("QUANTIZE_MAX_TASK");
        if (!string.IsNullOrEmpty(QUANTIZE_MAX_TASK))
        {
            int.TryParse(QUANTIZE_MAX_TASK, out _maxTask);
        }

        if (_maxTask < 0)
        {
            _maxTask = 1;
        }

        // TODO: 首次启动程序的时候需要加载未处理的量化数据
        await LoadingWikiDetailAsync();

        var tasks = new List<Task>();
        for (var i = 0; i < _maxTask; i++)
        {
            tasks.Add(Task.Factory.StartNew(WikiDetailHandlerAsync, stoppingToken));
        }

        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// 
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
    /// 处理量化
    /// </summary>
    /// <param name="wikiDetail"></param>
    /// <param name="service"></param>
    private async ValueTask HandlerAsync(WikiDetail wikiDetail, IServiceProvider service)
    {
        var fileStorageRepository = service.GetRequiredService<IFileStorageRepository>();
        var wikiRepository = service.GetRequiredService<IWikiRepository>();
        var wikiMemoryService = service.GetRequiredService<WikiMemoryService>();

        var wiki = await wikiRepository.FindAsync(x => x.Id == wikiDetail.WikiId);

        CacheWikiDetails.TryAdd(wikiDetail.Id.ToString(), new ValueTuple<WikiDetail, Wiki>(wikiDetail, wiki));

        if (wikiDetail.Mode == ProcessMode.Auto)
        {
            wikiDetail.MaxTokensPerLine = 300;
            wikiDetail.MaxTokensPerParagraph = 1000;
            wikiDetail.OverlappingTokens = 100;
        }

        // 获取知识库配置的模型，如果没有则使用默认模型
        var serverless = wikiMemoryService.CreateMemoryServerless(new SearchClientConfig(),
            wikiDetail.MaxTokensPerLine, wikiDetail.MaxTokensPerParagraph, wikiDetail.OverlappingTokens, wiki?.Model,
            wiki?.EmbeddingModel);
        try
        {
            _logger.LogInformation($"开始量化：{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId}");
            List<string> step = [];
            if (wikiDetail.TrainingPattern == TrainingPattern.QA)
            {
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

            string result = string.Empty;
            if (wikiDetail.Type == "file")
            {
                var fileInfoQuery = await fileStorageRepository.FindAsync(x => x.Id == wikiDetail.FileId);

                result = await serverless.ImportDocumentAsync(fileInfoQuery.FullName,
                    wikiDetail.Id.ToString(),
                    tags: new TagCollection()
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
                    }, "wiki", steps: step.ToArray());
            }
            else if (wikiDetail.Type == "web")
            {
                result = await serverless.ImportWebPageAsync(wikiDetail.Path,
                    wikiDetail.Id.ToString(),
                    tags: new TagCollection()
                    {
                        {
                            "wikiId", wikiDetail.WikiId.ToString()
                        },
                        {
                            "wikiDetailId", wikiDetail.Id.ToString()
                        }
                    }, "wiki", steps: step.ToArray());
            }
            else if (wikiDetail.Type == "data")
            {
                result = await serverless.ImportDocumentAsync(wikiDetail.Path,
                    wikiDetail.Id.ToString(),
                    tags: new TagCollection()
                    {
                        {
                            "wikiId", wikiDetail.WikiId.ToString()
                        },
                        {
                            "wikiDetailId", wikiDetail.Id.ToString()
                        }
                    }, "wiki", steps: step.ToArray());
            }

            await wikiRepository.UpdateDetailsState(wikiDetail.Id, WikiQuantizationState.Accomplish);
            _logger.LogInformation($"量化成功：{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId} {result}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"量化失败{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId}");

            if (wikiDetail.State != WikiQuantizationState.Fail)
            {
                await wikiRepository.UpdateDetailsState(wikiDetail.Id, WikiQuantizationState.Fail);
            }
        }
        finally
        {
            CacheWikiDetails.Remove(wikiDetail.Id.ToString(), out _);
        }
    }


    private async Task LoadingWikiDetailAsync()
    {
        using var asyncServiceScope = _serviceProvider.CreateScope();

        var wikiRepository = asyncServiceScope.ServiceProvider.GetRequiredService<IWikiRepository>();
        var mapper = asyncServiceScope.ServiceProvider.GetRequiredService<IMapper>();
        foreach (var wikiDetail in await wikiRepository.GetFailedDetailsAsync())
        {
            await AddWikiDetailAsync(wikiDetail);
        }
    }
}