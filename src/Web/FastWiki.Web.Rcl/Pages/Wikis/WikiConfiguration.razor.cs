using FastWiki.Web.Rcl.Helper;

namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class WikiConfiguration
{
    [Parameter] public WikiDto Value { get; set; } = new();


    public List<(string, string)> Models { get; set; } = ChatHelper.GetChatModel();

    private async Task SubmitAsync()
    {
        await WikiService.UpdateAsync(Value);

        await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("成功", "修改成功"));
    }
}
