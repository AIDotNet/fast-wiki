namespace FastWiki.Web.Rcl.Pages.Applications;

public partial class Application
{
    private int page = 1;

    private int pageSize = 20;

    private string Search;

    private PaginatedListBase<ChatApplicationDto> Result = new();

    private async Task Loading()
    {
        Result = await ChatApplicationService.GetListAsync(page, pageSize);

        _ = InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await Loading();
    }

    private void OpenChatApplication(ChatApplicationDto chatApplication)
    {
        NavigationManager.NavigateTo($"/application/{chatApplication.Id}");
    }

    private async Task Remove(string id)
    {
        await ChatApplicationService.RemoveAsync(id);
    }

    private async Task OnSearch()
    {
        page = 1;

        await Loading();

    }
}

