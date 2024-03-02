namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class Chat
{
    private PaginatedListBase<ChatApplicationDto> ChatApplications = new();

    private ChatApplicationDto chatApplication = new();

    private ChatDialogDto ChatDialog = new();

    private bool? _drawer;
    private void ArrowLeft()
    {
        NavigationManager.NavigateTo("/");
    }


    private async Task LoadingWiki()
    {
        ChatApplications = await ChatApplicationService.GetListAsync(1, int.MaxValue);
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadingWiki();
        }
    }
}