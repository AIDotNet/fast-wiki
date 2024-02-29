namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ShareChat
{
    [Parameter] [SupplyParameterFromQuery] public string Id { get; set; }

    private ChatDialogDto ChatDialog = new();

    private ChatApplicationDto ChatApplication = new();

    private string GuestId;

    private async Task LoadingChatApplication()
    {
        ChatApplication = await ChatApplicationService.GetChatShareApplicationAsync(Id);
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadingChatApplication();

            GuestId = await LocalStorageJsInterop.GetLocalStorageAsync(Constant.ChatShare);

            if (GuestId.IsNullOrWhiteSpace())
            {
                GuestId = Guid.NewGuid().ToString("N");

                await LocalStorageJsInterop.SetLocalStorageAsync(Constant.ChatShare, GuestId);
            }

            _ = InvokeAsync(StateHasChanged);
        }
    }
}