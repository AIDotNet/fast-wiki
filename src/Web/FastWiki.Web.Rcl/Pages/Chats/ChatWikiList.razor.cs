namespace FastWiki.Web.Rcl.Pages.Chats;

public partial class ChatWikiList
{
    [Parameter]
    public PaginatedListBase<WikiDto> Wikis { get; set; } 

    [Parameter]
    public WikiDto Value { get; set; }

    [Parameter]
    public EventCallback<WikiDto> ValueChanged { get; set; }

    private StringNumber _selectedItem;

    public StringNumber SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (value == null)
            {
                return;
            }
            _ = ValueChanged.InvokeAsync(Wikis.Result.FirstOrDefault(x => x.Id == value));
            _selectedItem = value;
        }
    }
}
