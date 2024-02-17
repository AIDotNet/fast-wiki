using FastWiki.Service.Contracts.Wikis.Dto;
using Masa.Utils.Models;

namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class WikiInfo
{
    [Parameter] public long Id { get; set; }

    private PaginatedListBase<WikiDetailDto> _wikiDetails = new();

    private int page = 1;
    private int pageSize = 10;

    private string keyword = "";

    private readonly List<DataTableHeader<WikiDetailDto>> _headers =
    [
        new()
        {
            Text = "文件名",
            Sortable = false,
            Value = nameof(WikiDetailDto.FileName)
        },

        new()
        {
            Text = "索引数量",
            Sortable = false, Value = nameof(WikiDetailDto.DataCount)
        },

        new()
        {
            Text = "数据类型",
            Sortable = false, Value = nameof(WikiDetailDto.Type)
        },

        new()
        {
            Text = "创建时间",
            Sortable = false, Value = nameof(WikiDetailDto.CreationTime)
        },

        new()
        {
            Text = "操作",
            Sortable = false,
        }
    ];

    private async Task LoadData()
    {
        _wikiDetails = await WikiService.GetWikiDetailsAsync(Id, keyword, page, pageSize);
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }
}