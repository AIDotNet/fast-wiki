using Masa.Blazor;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class WikiUploadWebPage
{
    [Parameter]
    public long Value { get; set; }

    [Parameter]
    public EventCallback<bool> OnSucceed { get; set; }

    private int _step = 1;
    private bool _visible;

    private string WebPage;

    private TrainingPattern _trainingPattern = TrainingPattern.Subsection;

    private ProcessMode _processMode = ProcessMode.Auto;
    /// <summary>
    /// 分段长度
    /// </summary>
    private int subsection = 512;
    private bool StartUpload;

    private List<CreateWikiDetailWebPageInput> Pages = new();

    private readonly List<DataTableHeader<CreateWikiDetailWebPageInput>> _headers =
    [
        new()
        {
            Text = "来源",
            Sortable = false,
            Value = nameof(CreateWikiDetailWebPageInput.Name)
        },
        new() {
            Text = "上传状态",
            Sortable = false,
            Value = nameof(CreateWikiDetailWebPageInput.Path)
        }
    ];

    private async Task SelectFileHandle()
    {
        _step = 2;
    }

    private async Task Upload(MouseEventArgs arg)
    {
        StartUpload = true;

        try
        {
            foreach (var item in Pages)
            {
                item.Subsection = subsection;
                item.TrainingPattern = _trainingPattern;
                item.Mode = _processMode;

                _ = InvokeAsync(StateHasChanged);
                
                await WikiService.CreateWikiDetailWebPageInputAsync(item);

                item.State = "完成";

                _ = InvokeAsync(StateHasChanged);

            }

            await PopupService.ConfirmAsync("成功", "上传完成", AlertTypes.Success);

            await OnSucceed.InvokeAsync(true);

        }
        catch (Exception e)
        {
            await PopupService.ConfirmAsync("失败", e.Message, AlertTypes.Error);
        }
        StartUpload = false;
    }

    private void DataHandler()
    {
        _step = 3;

        Pages = WebPage.Split('\n').Where(x => !x.IsNullOrEmpty()).Select(x => new CreateWikiDetailWebPageInput()
        {
            Name = x,
            Path = x,
            State= "未上传",
            WikiId = Value,
        }).ToList();
    }

    private void Remove(string text)
    {
        WebPage = WebPage.Replace(text,string.Empty);
    }
}
