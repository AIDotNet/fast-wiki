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
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var user = new ChatDialogHistoryDto()
        {
            ChatApplicationId = ChatApplication?.Id,
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
            ChatApplicationId = ChatApplication?.Id,
            ChatDialogId = chatDialogId,
            Content = string.Empty,
            CreationTime = DateTime.Now,
            Current = false,
            Id = Guid.NewGuid().ToString(),
            Type = ChatDialogHistoryType.Text
        };

        ChatDialogHistory.Result.Add(chat);

        await ChatApplicationService.CreateChatDialogHistoryAsync(new CreateChatDialogHistoryInput()
        {
            ChatApplicationId = user.ChatApplicationId,
            ChatDialogId = user.ChatDialogId,
            Content = user.Content,
            Current = user.Current,
            Type = user.Type
        });

        await foreach (var item in ChatApplicationService.CompletionsAsync(new CompletionsInput()
        {
            ChatDialogId = ChatDialogId,
            Content = value,
            ChatApplicationId = ChatApplication?.Id,
        }))
        {
            Console.Write(item.Content);
            chat.Content += item.Content;
            _ = InvokeAsync(StateHasChanged);
        }

        await ChatApplicationService.CreateChatDialogHistoryAsync(new CreateChatDialogHistoryInput()
        {
            ChatApplicationId = chat.ChatApplicationId,
            ChatDialogId = chat.ChatDialogId,
            Content = chat.Content,
            Current = chat.Current,
            Type = chat.Type
        });
    }

    private async Task RemoveHistoryAsync(string id)
    {
        await ChatApplicationService.RemoveDialogHistoryAsync(id);

        await LoadingChatDialogHistoryAsync();
    }
}
