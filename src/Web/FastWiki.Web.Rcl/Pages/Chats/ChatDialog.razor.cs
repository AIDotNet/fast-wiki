namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ChatDialog
{
    private string _chatId;

    [Parameter]
    public string ChatId
    {
        get => _chatId;
        set
        {
            if (value == ChatId)
            {
                return;
            }
            _chatId = value;
            _ = LoadingDialogAsync();
        }
    }

    [Parameter] public ChatDialogType Type { get; set; }

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
        _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_chatId);

        if (_chatDialogs.Count == 0 && _chatId != null)
        {
            await ChatApplicationService.CreateChatDialogAsync(new()
            {
                Name = "默认对话",
                ChatId = _chatId,
                Description = "默认创建的对话",
                Type = Type
            });

            _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_chatId);
        }

        SelectedItem = _chatDialogs.First().Id;

        _ = InvokeAsync(StateHasChanged);
    }

    private async Task CreateDialogAsync()
    {
        await ChatApplicationService.CreateChatDialogAsync(new()
        {
            Name = "新建对话",
            ChatId = _chatId,
            Description = "新建的对话",
            Type = Type
        });

        _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_chatId);
    }
}