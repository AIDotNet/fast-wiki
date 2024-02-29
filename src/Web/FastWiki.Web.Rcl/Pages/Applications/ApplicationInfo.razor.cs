namespace FastWiki.Web.Rcl.Pages.Applications;

public partial class ApplicationInfo
{
    [Parameter] public string Id { get; set; }

    private ChatApplicationDto Application { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Application = await ChatApplicationService.GetAsync(Id);
        }
    }
}