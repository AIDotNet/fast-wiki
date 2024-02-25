namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ChatDialog
{
    private long _value;

    [Parameter]
    public long Value
    {
        get => _value; set
        {
            _value = value;
            _ = LoadingDialogAsync();
        }
    }

    private async Task LoadingDialogAsync()
    {

    }
}
