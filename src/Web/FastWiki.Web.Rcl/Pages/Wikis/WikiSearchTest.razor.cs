namespace FastWiki.Web.Rcl.Pages.Wikis;

public partial class WikiSearchTest
{
    [Parameter]
    public long Value { get; set; }

    private string _search;

    private bool overlay;

    private SearchVectorQuantityResult _quantity = new ();

    private async Task SearchAsync()
    {
        try
        {
            overlay = true;
            _quantity = await WikiService.GetSearchVectorQuantityAsync(Value, _search, 0.0);

        }
        finally
        {
            overlay = false;
        }
    }
}
