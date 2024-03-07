using Microsoft.SemanticKernel.Text;
using System.Text;

namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class WikiUploadData
{
    private bool _visible;

    [Parameter]
    public long Value { get; set; }

    [Parameter]
    public EventCallback<bool> OnSucceed { get; set; }

    private int _step = 1;

    private string name;

    private string data;

    /// <summary>
    /// 分段长度
    /// </summary>
    private int subsection = 512;

    private List<string> datas = new();

    private TrainingPattern _trainingPattern = TrainingPattern.Subsection;

    private ProcessMode _processMode = ProcessMode.Auto;

    private void DataHandle()
    {
        _step = 2;

        Preview();
    }

    private void Preview()
    {

        if (_processMode == ProcessMode.Auto)
            subsection = 512;

        if (_trainingPattern == TrainingPattern.Subsection)
        {
            datas.Clear();

#pragma warning disable SKEXP0055 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            var lines = TextChunker.SplitPlainTextLines(data, subsection);
#pragma warning restore SKEXP0055 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            datas.AddRange(lines);
        }
    }

    private async Task Handler()
    {

        // 将字符串转换Stream
        var stream = new MemoryStream(Encoding.UTF8.GetBytes(data));

        var fileInfo = await StorageService.UploadFile(stream, name+".md");
        var input = new CreateWikiDetailsInput()
        {
            Name = name,
            WikiId = Value,
            FileId = fileInfo.Id,
            FilePath = fileInfo.Path,
            Subsection = subsection,
            Mode = _processMode,
            TrainingPattern = _trainingPattern
        };
        await WikiService.CreateWikiDetailsAsync(input);

        await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("提示", "上传成功"));
    }
}
