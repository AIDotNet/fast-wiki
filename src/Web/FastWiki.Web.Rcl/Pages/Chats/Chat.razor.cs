namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class Chat
{
    private PaginatedListBase<WikiDto> Wikis = new();

    private WikiDto Wiki;

    private void ArrowLeft()
    {
        NavigationManager.NavigateTo("/");
    }


    private async Task LoadingWiki()
    {
        Wikis =await WikiService.GetWikiListAsync(string.Empty, 1, int.MaxValue);
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadingWiki();
    }
}
