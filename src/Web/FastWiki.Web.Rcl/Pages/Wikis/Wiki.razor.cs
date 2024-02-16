using FastWiki.Service.Contracts.Wikis.Dto;
using Masa.Blazor.Presets;
using Masa.Utils.Models;

namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class Wiki
{
    private string Search { get; set; } = string.Empty;

    private int page = 1;

    private int pageSize = 10;

    private PaginatedListBase<WikiDto> Result { get; set; } = new();

    private async Task OnSearch()
    {
        page = 1;
        await LoadingData();
    }

    public async Task LoadingData()
    {
        Result = await WikiService.GetWikiListAsync(Search, page, pageSize);
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadingData();
    }

    private async Task Remove(long id)
    {
        await WikiService.RemoveAsync(id);

       await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("删除成功", AlertTypes.Info));

       await LoadingData();
    }

    public void OpenWiki(WikiDto wiki)
    {
        NavigationManager.NavigateTo("/wiki/" + wiki.Id);
    }
}