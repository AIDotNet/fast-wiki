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
        if (ChatApplications.Result.Count == 0)
        {
            await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("请先创建应用",AlertTypes.Error));
            NavigationManager.NavigateTo("/application");
            return;
        }

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