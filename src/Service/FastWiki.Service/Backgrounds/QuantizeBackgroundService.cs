using System.Threading.Channels;
using FastWiki.Service.Domain.Wikis.Aggregates;
using FastWiki.Service.Service;

namespace FastWiki.Service.Backgrounds;

/// <summary>
/// 后台任务，用于量化
/// </summary>
public sealed class QuantizeBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 当前任务数量
    /// </summary>
    private static int _currentTask = 0;

    /// <summary>
    /// 最大量化任务数量
    /// </summary>
    private static int _maxTask = 1;

    private static readonly Channel<QuantizeWikiDetail> WikiDetails = Channel.CreateBounded<QuantizeWikiDetail>(
        new BoundedChannelOptions(1000)
        {
            SingleReader = true,
            SingleWriter = false
        });

    /// <summary>
    /// 构造
    /// </summary>
    public QuantizeBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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
    public static async Task AddWikiDetailAsync(QuantizeWikiDetail wikiDetail)
    {
        await WikiDetails.Writer.WriteAsync(wikiDetail);
    }

    private async Task WikiDetailHandlerAsync()
    {
        using var asyncServiceScope = _serviceProvider.CreateScope();
        while (await WikiDetails.Reader.WaitToReadAsync())
        {
            Interlocked.Increment(ref _currentTask);
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
    private static async ValueTask HandlerAsync(QuantizeWikiDetail wikiDetail, IServiceProvider service)
    {
        var fileStorageRepository = service.GetRequiredService<IFileStorageRepository>();
        var wikiRepository = service.GetRequiredService<IWikiRepository>();
        var wikiMemoryService = service.GetRequiredService<WikiMemoryService>();
        var wiki = await wikiRepository.FindAsync(x => x.Id == wikiDetail.WikiId);
        if (wikiDetail.Subsection == 0)
        {
            wikiDetail.Subsection = 512;
        }

        // 获取知识库配置的模型，如果没有则使用默认模型
        var serverless = wikiMemoryService.CreateMemoryServerless(new SearchClientConfig(),
        wikiDetail.Mode == ProcessMode.Auto ? 512 : wikiDetail.Subsection, wiki?.Model, wiki?.EmbeddingModel);
        try
        {
            Console.WriteLine($"开始量化：ʼ{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId}");

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
                    }, "wiki");
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
                    }, "wiki");
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
                    }, "wiki");
            }

            await wikiRepository.UpdateDetailsState(wikiDetail.Id, WikiQuantizationState.Accomplish);
            Console.WriteLine($"量化成功：{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId} {result}");
        }
        catch (Exception e)
        {
            Console.WriteLine(
            $"量化失败{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId} {Environment.NewLine + e.Message}");

            // TODO: 由于api可能存在限流，如果出现异常大概率是限流导致，在这里等待一会
            await Task.Delay(500);

            if (wikiDetail.State != WikiQuantizationState.Fail)
            {
                await wikiRepository.UpdateDetailsState(wikiDetail.Id, WikiQuantizationState.Fail);
            }

        }

    }

    private async Task LoadingWikiDetailAsync()
    {
        using var asyncServiceScope = _serviceProvider.CreateScope();

        var wikiRepository = asyncServiceScope.ServiceProvider.GetRequiredService<IWikiRepository>();
        var mapper = asyncServiceScope.ServiceProvider.GetRequiredService<IMapper>();
        foreach (var wikiDetail in await wikiRepository.GetFailedDetailsAsync())
        {
            await AddWikiDetailAsync(mapper.Map<QuantizeWikiDetail>(wikiDetail));
        }
    }
}

public sealed class QuantizeWikiDetail : WikiDetail
{
    public int Subsection { get; set; }

    public ProcessMode Mode { get; set; } = ProcessMode.Auto;

    public TrainingPattern TrainingPattern { get; set; } = TrainingPattern.Subsection;
}