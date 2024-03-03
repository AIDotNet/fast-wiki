namespace FastWiki.Web.Rcl.Pages.Applications;

public partial class ApplicationSessionLog
{

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
            Text = "索引数量",
            Sortable = false, Value = nameof(ChatDialogDto.Description)
        },
        new()
        {
            Text = "对话来源",
            Sortable = false, Value = nameof(ChatDialogDto.Type)
        },

        new()
        {
            Text = "操作",
            Sortable = false,
        }
    ];

    [Parameter]
    public string ChatApplicationId { get; set; }

    private List<ChatDialogDto> chatDialogs = new();

    private async Task LoadingAsync()
    {
        chatDialogs = await ChatApplicationService.GetChatDialogAsync(ChatApplicationId, true);

        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadingAsync();
        }
    }
}
