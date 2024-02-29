using Masa.Blazor;

namespace FastWiki.Web.Rcl.Pages.Applications;

public partial class PublishChatShare
{
    private ChatApplicationDto value;

    [Parameter]
    public ChatApplicationDto Value
    {
        get => value;
        set
        {
            this.value = value;
            page = 1;
            _ = LoadingAsync();
        }
    }

    private PaginatedListBase<ChatShareDto> chatShares = new();

    private int page = 1;

    private int pageSize = 20;

    private readonly List<DataTableHeader<ChatShareDto>> _headers =
    [
        new()
        {
            Text = "#",
            Sortable = false,
            Value = nameof(ChatShareDto.Id)
        },

        new()
        {
            Text = "文件名",
            Sortable = false,
            Value = nameof(ChatShareDto.Name)
        },

        new()
        {
            Text = "索引数量",
            Sortable = false, Value = nameof(ChatShareDto.Expires)
        },

        new()
        {
            Text = "数据类型",
            Sortable = false, Value = nameof(ChatShareDto.AvailableToken)
        },

        new()
        {
            Text = "创建时间",
            Sortable = false, Value = nameof(ChatShareDto.AvailableQuantity)
        },
        new()
        {
            Text = "操作",
            Sortable = false,
        }
    ];


    private async Task OnPageChanged(int page)
    {
        if (this.page == page)
            return;

        this.page = page;

    }

    private async Task ShareAsync(ChatShareDto item)
    {
        var share = NavigationManager.BaseUri.TrimEnd('/') + "/chat/share-chat?id=" + item.Id;

       await JsHelperModule.CopyText(share);


       await PopupService.EnqueueSnackbarAsync("成功", "复制成功", AlertTypes.Success);
    }

    private async Task LoadingAsync()
    {
        if (!string.IsNullOrEmpty(Value.Id))
        {
            chatShares = await ChatApplicationService.GetChatShareListAsync(Value.Id, page, pageSize);
            _ = InvokeAsync(StateHasChanged);
        }
    }

    private async Task CreateChatShareAsync(CreateChatShareInput input)
    {
        input.ChatApplicationId = Value.Id;
        await ChatApplicationService.CreateShareAsync(input);

        await PopupService.EnqueueSnackbarAsync("成功", "创建分享对话成功", AlertTypes.Success);

        await LoadingAsync();
    }
}
