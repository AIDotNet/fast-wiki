namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class Chat
{
    private PaginatedListBase<ChatApplicationDto> ChatApplications = new();

    private ChatApplicationDto chatApplication = new();

    private ChatDialogDto ChatDialog = new();

    private void ArrowLeft()
    {
        NavigationManager.NavigateTo("/");
    }


    private async Task LoadingWiki()
    {
        ChatApplications = await ChatApplicationService.GetListAsync(1, int.MaxValue);
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadingWiki();
    }
}