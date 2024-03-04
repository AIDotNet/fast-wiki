using System.Threading.Channels;
using FastWiki.Service.Service;

namespace FastWiki.Service.Backgrounds;

public sealed class QuantizeBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 当前任务数量
    /// </summary>
    private static int CurrentTask = 0;

    /// <summary>
    /// 最大量化任务数量
    /// </summary>
    public static int MaxTask = 3;

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
            int.TryParse(QUANTIZE_MAX_TASK, out MaxTask);
        }

        // TODO: 首次启动程序的时候需要加载未处理的量化数据
        await LoadingWikiDetailAsync();
        await Task.Factory.StartNew(WikiDetailHandlerAsync, stoppingToken);
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
        while (await WikiDetails.Reader.WaitToReadAsync())
        {
            var wikiDetail = await WikiDetails.Reader.ReadAsync();

            if (wikiDetail != null)
            {
                for (var i = 0; i < 200; i++)
                {
                    if (CurrentTask < MaxTask)
                    {
                        _ = HandlerAsync(wikiDetail);
                        break;
                    }

                    await Task.Delay(500);
                }
            }
        }
    }

    /// <summary>
    /// 处理量化
    /// </summary>
    /// <param name="state"></param>
    private async ValueTask HandlerAsync(object state)
    {
        Interlocked.Increment(ref CurrentTask);
        if (state is QuantizeWikiDetail wikiDetail)
        {
            using var asyncServiceScope = _serviceProvider.CreateScope();

            var fileStorageRepository = asyncServiceScope.ServiceProvider.GetRequiredService<IFileStorageRepository>();
            var wikiRepository = asyncServiceScope.ServiceProvider.GetRequiredService<IWikiRepository>();
            var wikiMemoryService = asyncServiceScope.ServiceProvider.GetRequiredService<WikiMemoryService>();
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

                await AddWikiDetailAsync(wikiDetail);
            }
        }

        Interlocked.Decrement(ref CurrentTask);
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