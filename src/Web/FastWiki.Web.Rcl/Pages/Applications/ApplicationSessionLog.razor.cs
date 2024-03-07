namespace FastWiki.Web.Rcl.Pages.Applications;

public partial class ApplicationSessionLog
{
    private int page = 1;
    private int pageSize = 10;

    private readonly List<DataTableHeader<ChatDialogDto>> _headers =
    [
        new()
        {
            Text = "#",
            Sortable = false,
            Value = nameof(ChatDialogDto.Id)
        },

        new()
        {
            Text = "对话名称",
            Sortable = false,
            Value = nameof(ChatDialogDto.Name)
        },

        new()
        {
            Text = "描述",
            Sortable = false, Value = nameof(ChatDialogDto.Description)
        },
        new()
        {
            Text = "对话来源",
            Sortable = false, Value = nameof(ChatDialogDto.Type)
        },
        new()
        {
            Text = "创建时间",
            Sortable = false, Value = nameof(ChatDialogDto.CreationTime)
        },
        new()
        {
            Text = "操作",
            Sortable = false,
        }
    ];

    [Parameter]
    public string ChatApplicationId { get; set; }

    private PaginatedListBase<ChatDialogDto> chatDialogs = new();

    private async Task LoadingAsync()
    {
        chatDialogs = await ChatApplicationService.GetSessionLogDialogAsync(ChatApplicationId, page,pageSize);

        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadingAsync();
        }
    }
    private void OnPageChanged(int page)
    {
        if (this.page == page)
            return;

        this.page = page;

    }
}
