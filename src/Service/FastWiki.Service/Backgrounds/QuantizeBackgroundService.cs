using System.Threading.Channels;
using FastWiki.Service.Service;

namespace FastWiki.Service.Backgrounds;

public sealed class QuantizeBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// ???????????
    /// </summary>
    private static int CurrentTask = 0;

    /// <summary>
    /// ???????????
    /// </summary>
    public static int MaxTask = 3;

    private static readonly Channel<QuantizeWikiDetail> WikiDetails = Channel.CreateBounded<QuantizeWikiDetail>(
        new BoundedChannelOptions(1000)
        {
            SingleReader = true,
            SingleWriter = false
        });

    /// <summary>
    /// ??????
    /// </summary>
    public QuantizeBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// ???????
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // ?????????????????????????????
        var QUANTIZE_MAX_TASK = Environment.GetEnvironmentVariable("QUANTIZE_MAX_TASK");
        if (!string.IsNullOrEmpty(QUANTIZE_MAX_TASK))
        {
            int.TryParse(QUANTIZE_MAX_TASK, out MaxTask);
        }

        // TODO: ?????????????????wikiDetail
        await LoadingWikiDetailAsync();
        await Task.Factory.StartNew(WikiDetailHandlerAsync, stoppingToken);
    }

    /// <summary>
    /// ????wikiDetail??????
    /// </summary>
    /// <param name="wikiDetail"></param>
    public static async Task AddWikiDetailAsync(QuantizeWikiDetail wikiDetail)
    {
        await WikiDetails.Writer.WriteAsync(wikiDetail);
    }

    private async Task WikiDetailHandlerAsync()
    {
        // wikiDetails???????
        while (await WikiDetails.Reader.WaitToReadAsync())
        {
            var wikiDetail = await WikiDetails.Reader.ReadAsync();

            if (wikiDetail != null)
            {
                // ????????С????????????? ?????????
                for (var i = 0; i < 200; i++)
                {
                    if (CurrentTask < MaxTask)
                    {
                        _ = HandlerAsync(wikiDetail);
                        break;
                    }

                    // ??? 1s
                    await Task.Delay(500);
                }
            }
        }
    }

    /// <summary>
    /// ????wikiDetail??????????
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
            
            if(wikiDetail.Subsection == 0)
            {
                wikiDetail.Subsection = 512;
            }
            var serverless = wikiMemoryService.CreateMemoryServerless(new SearchClientConfig(),
                wikiDetail.Mode == ProcessMode.Auto ? 512 : wikiDetail.Subsection);
            
            try
            {
                Console.WriteLine($"量化数据开始{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId}");

                string result = string.Empty;
                if (wikiDetail.Type == "file")
                {
                    // ????wikiDetail
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
                Console.WriteLine($"量化完成{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId} {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"异常数据{wikiDetail.FileName} {wikiDetail.Path} {wikiDetail.FileId} {Environment.NewLine + e.Message}");

                // TODO: 可能出现限流导致异常，这里需要等待
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

        // ???????wikiDetail
        var wikiDetails = await wikiRepository.GetFailedDetailsAsync();

        foreach (var wikiDetail in wikiDetails)
        {
            await AddWikiDetailAsync(mapper.Map<QuantizeWikiDetail>(wikiDetail));
        }
    }
}

public class QuantizeWikiDetail : WikiDetail
{
    public int Subsection { get; set; }

    public ProcessMode Mode { get; set; } = ProcessMode.Auto;

    public TrainingPattern TrainingPattern { get; set; } = TrainingPattern.Subsection;
}