namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ChatDialog
{
    private string _applicationId;

    [Parameter]
    public string ApplicationId
    {
        get => _applicationId;
        set
        {
            if (value == ApplicationId)
            {
                return;
            }
            _applicationId = value;
            _ = LoadingDialogAsync();
        }
    }

    /// <summary>
    /// 游客Id可空
    /// </summary>
    [Parameter] public string? ChatId { get; set; }

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
        _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_applicationId);

        if (_chatDialogs.Count == 0 && _applicationId != null)
        {
            await ChatApplicationService.CreateChatDialogAsync(new()
            {
                Name = "默认对话",
                ChatId = ChatId,
                Description = "默认创建的对话",
                Type = Type,
                ApplicationId = _applicationId
            });

            _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_applicationId);
        }

        SelectedItem = _chatDialogs.First().Id;

        _ = InvokeAsync(StateHasChanged);
    }

    private async Task CreateDialogAsync()
    {
        await ChatApplicationService.CreateChatDialogAsync(new()
        {
            Name = "新建对话",
            ChatId = _applicationId,
            Description = "新建的对话",
            Type = Type
        });

        _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_applicationId);
    }
}