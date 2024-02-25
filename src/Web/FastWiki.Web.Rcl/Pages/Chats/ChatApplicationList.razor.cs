namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ChatApplicationList
{
    private PaginatedListBase<ChatApplicationDto> _chatApplication;

    [Parameter]
    public PaginatedListBase<ChatApplicationDto> ChatApplication
    {
        get => _chatApplication;
        set
        {
            _chatApplication = value;
            if (SelectedItem == null && value.Result != null)
            {
                SelectedItem = value.Result.FirstOrDefault()?.Id;
            }
        }
    }

    [Parameter] public ChatApplicationDto Value { get; set; }

    [Parameter] public EventCallback<ChatApplicationDto> ValueChanged { get; set; }

    private StringNumber _selectedItem;

    public StringNumber SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (value == null || ChatApplication.Result == null) 
            {
                return;
            }

            _ = ValueChanged.InvokeAsync(ChatApplication.Result.FirstOrDefault(x => x.Id == value));
            _selectedItem = value;
        }
    }
}