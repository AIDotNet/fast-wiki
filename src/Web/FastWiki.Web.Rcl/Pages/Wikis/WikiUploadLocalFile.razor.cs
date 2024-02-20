using FastWiki.Service.Contracts.Wikis;
using Microsoft.SemanticKernel.Text;

namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class WikiUploadLocalFile
{
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

    /// <summary>
    /// 最多支持文件数量
    /// </summary>
    private const int MaxFilesCount = 100;

    private int _step = 1;

    private List<IBrowserFile> _files = [];

    private TrainingPattern _trainingPattern = TrainingPattern.Subsection;

    private ProcessMode _processMode = ProcessMode.Auto;

    private List<SubsectionInput> _inputs = new();

    /// <summary>
    /// 分段长度
    /// </summary>
    private int subsection = 512;

    private void UploadChanged(InputFileChangeEventArgs args)
    {
        _files.AddRange(args.GetMultipleFiles(MaxFilesCount).Where(x => _types.Contains(x.ContentType)).ToList());
        InvokeAsync(StateHasChanged);
    }

    private void RemoveFile(IBrowserFile file)
    {
        _files.Remove(file);
    }

    private async Task Preview()
    {
        if (_trainingPattern == TrainingPattern.Subsection)
        {
            foreach (IBrowserFile file in _files)
            {
                var read = new StreamReader(file.OpenReadStream());

#pragma warning disable SKEXP0055 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
                var lines = TextChunker.SplitPlainTextLines(await read.ReadToEndAsync(), subsection);
#pragma warning restore SKEXP0055 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
                _inputs.Add(new SubsectionInput()
                {
                    Name = file.Name,
                    Lins = lines,
                    Data = file
                });

            }


        }
    }
}