namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ChatDialog
{
    private string _chatApplicationId;

    [Parameter]
    public string ChatApplicationId
    {
        get => _chatApplicationId;
        set
        {
            if (value == ChatApplicationId)
            {
                return;
            }
            _chatApplicationId = value;
            _ = LoadingDialogAsync();
        }
    }

    [Parameter] public ChatDialogDto Value { get; set; }

    [Parameter] public EventCallback<ChatDialogDto> ValueChanged { get; set; }

    private StringNumber _selectedItem;

    public StringNumber SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (value == null || value == _selectedItem)
            {
                return;
            }

            _ = ValueChanged.InvokeAsync(_chatDialogs.FirstOrDefault(x => x.Id == value.ToString()));
            _selectedItem = value;
        }
    }

    private List<ChatDialogDto> _chatDialogs = new();

    private async Task LoadingDialogAsync()
    {
        _chatDialogs = await ChatApplicationService.GetChatDialogAsync();

        if (_chatDialogs.Count == 0 && _chatApplicationId != null)
        {
            await ChatApplicationService.CreateChatDialogAsync(new()
            {
                Name = "默认对话",
                ChatApplicationId = _chatApplicationId,
                Description = "默认创建的对话"
            });

            _chatDialogs = await ChatApplicationService.GetChatDialogAsync();
        }

        SelectedItem = _chatDialogs.First().Id;

        _ = InvokeAsync(StateHasChanged);
    }

    private async Task CreateDialogAsync()
    {
        await ChatApplicationService.CreateChatDialogAsync(new()
        {
            Name = "新建对话",
            ChatApplicationId = _chatApplicationId,
            Description = "新建的对话"
        });

        _chatDialogs = await ChatApplicationService.GetChatDialogAsync();
    }
}