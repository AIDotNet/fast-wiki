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

    private string? _chatId;

    /// <summary>
    /// 游客Id可空
    /// </summary>
    [Parameter]
    public string? ChatId
    {
        get => _chatId;
        set
        {
            if (value.IsNullOrWhiteSpace() || _chatId == value)
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

            _selectedItem = value;
            Value = _chatDialogs.FirstOrDefault(x => x.Id == value.ToString());
        }
    }

    private List<ChatDialogDto> _chatDialogs = new();

    private async Task LoadingDialogAsync()
    {
        if (ChatId.IsNullOrWhiteSpace())
        {
            _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_applicationId, false);
        }
        else
        {
            _chatDialogs = await ChatApplicationService.GetChatShareDialogAsync(ChatId);
        }

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

            if (ChatId.IsNullOrWhiteSpace())
            {
                _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_applicationId, false);
            }
            else
            {
                _chatDialogs = await ChatApplicationService.GetChatShareDialogAsync(ChatId);
            }

        }

        SelectedItem = _chatDialogs.First().Id;

        _ = InvokeAsync(StateHasChanged);
    }

    private async Task CreateDialogAsync()
    {
        await ChatApplicationService.CreateChatDialogAsync(new()
        {
            Name = "新建对话",
            ChatId = ChatId,
            Description = "新建的对话",
            Type = Type,
            ApplicationId = _applicationId
        });

        if (ChatId.IsNullOrWhiteSpace())
        {
            _chatDialogs = await ChatApplicationService.GetChatDialogAsync(_applicationId, false);
        }
        else
        {
            _chatDialogs = await ChatApplicationService.GetChatShareDialogAsync(ChatId);
        }

    }

    private async Task RemoveDialogAsync(string id)
    {
        if (_chatDialogs.Count < 2)
        {
            await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("提示", "对话请至少保留一个！"));
            return;
        }

        if (ChatId.IsNullOrWhiteSpace())
        {
            await ChatApplicationService.RemoveDialogAsync(id);
        }
        else
        {
            await ChatApplicationService.RemoveShareDialogAsync(ChatId,id);
        }

        _ = LoadingDialogAsync();
    }

    private void RenameAsync(ChatDialogDto item)
    {
        item.IsEdit = true;
    }

    private async Task UpdateDialogAsync(ChatDialogDto item)
    {
        item.IsEdit = false;
        if (ChatId.IsNullOrWhiteSpace())
        {
            await ChatApplicationService.UpdateDialogAsync(item);
        }
        else
        {
            item.ChatId = ChatId;
            await ChatApplicationService.UpdateShareDialogAsync(item);
        }

    }
}