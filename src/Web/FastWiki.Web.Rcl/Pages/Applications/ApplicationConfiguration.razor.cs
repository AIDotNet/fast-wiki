using FastWiki.Infrastructure.Common.Helper;

namespace FastWiki.Web.Rcl.Pages.Applications;

public partial class ApplicationConfiguration
{
    [Parameter]
    public ChatApplicationDto Value { get; set; }

    public PaginatedListBase<WikiDto> Wikis { get; set; } = new();
    private int page = 1;
    private int pageSize = 100;

    public List<(string, string)> Models = ChatHelper.GetChatModel();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadingAsync();
        }
    }

    private async Task LoadingAsync()
    {
        Wikis = await WikiService.GetWikiListAsync(string.Empty, page, pageSize);
    }

    private void SelectWiki(WikiDto wiki)
    {
        if (Value.WikiIds.All(x => wiki.Id != x))
        {
            Value.WikiIds.Add(wiki.Id);
        }
    }

    private void RemoveWiki(long wikiId)
    {
        Value.WikiIds.Remove(wikiId);
    }

    private async Task SubmitAsync()
    {
        await ChatApplicationService.UpdateAsync(new UpdateChatApplicationInput()
        {
            ChatModel = Value.ChatModel,
            Id = Value.Id,
            MaxResponseToken = Value.MaxResponseToken,
            Name = Value.Name,
            WikiIds = Value.WikiIds,
            Opener = Value.Opener,
            Parameter = Value.Parameter,
            Prompt = Value.Prompt,
            Temperature = Value.Temperature,
            Template = Value.Template
        });

        await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("修改成功", AlertTypes.Success));
    }
}
