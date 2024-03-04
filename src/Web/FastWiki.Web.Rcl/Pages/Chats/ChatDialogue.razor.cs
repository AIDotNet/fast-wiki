namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ChatDialogue
{
    private string chatDialogId;

    [Parameter]
    public string ChatDialogId
    {
        get => chatDialogId;
        set
        {
            if (chatDialogId == value || string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            page = 1;
            pageSize = 10;
            chatDialogId = value;
            _ = LoadingChatDialogHistoryAsync();
        }
    }

    [Parameter] public ChatApplicationDto ChatApplication { get; set; }

    /// <summary>
    /// 对话分享Id 如果存在则使用分享
    /// </summary>
    [Parameter] public string ChatSharedId { get; set; }

    private int page = 1;

    private int pageSize = 10;

    private PaginatedListBase<ChatDialogHistoryDto> ChatDialogHistory = new()
    {
        Result = []
    };

    private async Task LoadingChatDialogHistoryAsync()
    {
        ChatDialogHistory = await ChatApplicationService.GetChatDialogHistoryAsync(ChatDialogId, page, pageSize);
        await InvokeAsync(StateHasChanged);
    }

    private async Task Submit(string value)
    {
        var user = new ChatDialogHistoryDto()
        {
            ChatDialogId = chatDialogId,
            Content = value,
            CreationTime = DateTime.Now,
            Current = true,
            Id = Guid.NewGuid().ToString(),
            Type = ChatDialogHistoryType.Text
        };

        ChatDialogHistory.Result.Add(user);

        var chat = new ChatDialogHistoryDto()
        {
            ChatDialogId = chatDialogId,
            Content = string.Empty,
            CreationTime = DateTime.Now,
            Current = false,
            Id = Guid.NewGuid().ToString(),
            Type = ChatDialogHistoryType.Text
        };

        ChatDialogHistory.Result.Add(chat);

        if (ChatSharedId.IsNullOrWhiteSpace())
        {
            await foreach (var item in ChatApplicationService.CompletionsAsync(new CompletionsInput()
            {
                ChatDialogId = ChatDialogId,
                ChatId = ChatApplication.Id,
                Content = value,
            }))
            {
                chat.Content += item.Content;
                if (item.SourceFile?.Count > 0)
                {
                    chat.SourceFile.AddRange(item.SourceFile);
                }

                await ScrollToBottom();
            }
        }
        else
        {
            await foreach (var item in ChatApplicationService.ChatShareCompletionsAsync(new()
            {
                ChatDialogId = ChatDialogId,
                ChatShareId = ChatSharedId,
                Content = value,
            }))
            {
                chat.Content += item.Content;
                if (item.SourceFile?.Count > 0)
                {
                    chat.SourceFile.AddRange(item.SourceFile);
                }

                await ScrollToBottom();
            }
        }

        await ScrollToBottom();

        await ChatApplicationService.CreateChatDialogHistoryAsync(new CreateChatDialogHistoryInput()
        {
            ChatDialogId = user.ChatDialogId,
            Content = user.Content,
            Current = user.Current,
            Type = user.Type
        });

        await ChatApplicationService.CreateChatDialogHistoryAsync(new CreateChatDialogHistoryInput()
        {
            ChatDialogId = chat.ChatDialogId,
            Content = chat.Content,
            Current = chat.Current,
            Type = chat.Type
        });
    }

    private async Task ScrollToBottom()
    {
        await JsHelperJsInterop.ScrollToBottom("chat-dialogue");
        await InvokeAsync(StateHasChanged);
    }

    private async Task RemoveHistoryAsync(string id)
    {
        await ChatApplicationService.RemoveDialogHistoryAsync(id);

        await LoadingChatDialogHistoryAsync();
    }

    private async Task OpenSourceFile(SourceFileDto item)
    {
        await JsHelperJsInterop.OpenUrl(item.FilePath);
    }

    private async Task OnCopy(string text)
    {
        await JsHelperModule.CopyText(text);
        await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("成功", "复制成功"));
    }
}
