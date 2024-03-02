using FastWiki.Service.Contracts.Wikis;
using Masa.Blazor;
using Microsoft.SemanticKernel.Text;

namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class WikiUploadLocalFile 
{
    [Parameter]
    public long Value { get; set; }

    [Parameter]
    public EventCallback<bool> OnSucceed { get; set; }

    private bool _visible;

    private static readonly List<string> _types =
    [
        "text/plain",
        "application/msword",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        "application/vnd.ms-excel",
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        "application/vnd.ms-powerpoint",
        "application/vnd.openxmlformats-officedocument.presentationml.presentation",
        "application/pdf",
        "application/json",
        "text/markdown"
    ];

    private static readonly List<string> NameSuffix =
        [
            ".md",
            ".txt",
            ".pdf"
        ];

    public bool Directory { get; set; }

    /// <summary>
    /// 最多支持文件数量
    /// </summary>
    private const int MaxFilesCount = 100;

    private int _step = 1;

    private readonly Dictionary<IBrowserFile, List<SubsectionInput>>  _files = [];

    public List<UploadSubsectionInput> BrowserFiles = new();

    private TrainingPattern _trainingPattern = TrainingPattern.Subsection;

    private ProcessMode _processMode = ProcessMode.Auto;


    private readonly List<DataTableHeader<UploadSubsectionInput>> _headers =
    [
        new()
        {
            Text = "文件名",
            Sortable = false,
            Value = nameof(UploadSubsectionInput.Name)
        },
        new() {
            Text = "分段数量",
            Sortable = false,
            Value = nameof(UploadSubsectionInput.Count)
        },
        new() {
            Text = "文件上传进度",
            Sortable = false,
            Value = nameof(UploadSubsectionInput.FileProgress)
        },
        new() {
            Text = "数据上传进度",
            Sortable = false,
            Value = nameof(UploadSubsectionInput.DataProgress)
        }
    ];

    /// <summary>
    /// 分段长度
    /// </summary>
    private int subsection = 512;

    private void UploadChanged(InputFileChangeEventArgs args)
    {
        _files.Clear();
        foreach (var item in args.GetMultipleFiles(int.MaxValue)
                     .Take(MaxFilesCount).Where(x => _types.Contains(x.ContentType) || NameSuffix.Any(n => x.Name.EndsWith(n))).ToList())
        {
            _files.Add(item, []);
        }
        InvokeAsync(StateHasChanged);
    }

    private void RemoveFile(IBrowserFile file)
    {
        _files.Remove(file);
    }

    private async Task SelectFileHandle()
    {
        _step = 2;
        await Preview();
    }

    private async Task Preview()
    {
        if (_processMode == ProcessMode.Auto)
            subsection = 512;

        if (_trainingPattern == TrainingPattern.Subsection)
        {
            BrowserFiles.Clear();
            foreach (var item in _files)
            {
                var read = new StreamReader(item.Key.OpenReadStream());
                item.Value.Clear();
#pragma warning disable SKEXP0055 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
                var lines = TextChunker.SplitPlainTextLines(await read.ReadToEndAsync(), subsection);
#pragma warning restore SKEXP0055 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
                item.Value.AddRange(lines.Select(x => new SubsectionInput(x, string.Empty, item.Key.Name)));

                BrowserFiles.Add(new UploadSubsectionInput()
                {
                    Count = item.Value.Count,
                    Name = item.Key.Name,
                });
            }
        }
    }

    private async Task Upload()
    {
        foreach (var file in _files)
        {
            var fileItem = BrowserFiles.FirstOrDefault(x => x.Name == file.Key.Name);

            fileItem!.FileProgress = 1;

            var fileInfo = await StorageService.UploadFile(file.Key.OpenReadStream(), file.Key.Name);
            var input = new CreateWikiDetailsInput()
            {
                Name = file.Key.Name,
                WikiId = Value,
                FileId = fileInfo.Id,
                FilePath = fileInfo.Path,
                Subsection = subsection,
                Mode = _processMode,
                TrainingPattern = _trainingPattern
            };

            fileItem!.FileProgress = 100;

            _ = InvokeAsync(StateHasChanged);

            fileItem.DataProgress = 1;

            await WikiService.CreateWikiDetailsAsync(input);

            fileItem.DataProgress = 100;

            _ = InvokeAsync(StateHasChanged);
        }

        await PopupService.ConfirmAsync("成功", "上传完成", AlertTypes.Success);

        await OnSucceed.InvokeAsync(true);
    }

}