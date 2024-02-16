namespace Masa.Blazor.Pro.Global;

public class NavHelper
{
    private List<NavModel> _navList;
    private NavigationManager _navigationManager;

    public List<NavModel> Navs { get; } = new();

    public List<NavModel> SameLevelNavs { get; } = new();

    public List<PageTabItem> PageTabItems { get; } = new();

    public string CurrentUri => _navigationManager.Uri;

    public NavHelper(List<NavModel> navList, NavigationManager navigationManager)
    {
        _navList = navList;
        _navigationManager = navigationManager;
        Initialization();
    }

    private void Initialization()
    {
        _navList.ForEach(nav =>
        {
            if (nav.Hide is false) Navs.Add(nav);

            if (nav.Children is not null)
            {
                nav.Children = nav.Children.Where(c => c.Hide is false).ToArray();

                nav.Children.ForEach(child =>
                {
                    child.ParentId = nav.Id;
                    child.FullTitle = $"{nav.Title} {child.Title}";
                    child.ParentIcon = nav.Icon;
                });
            }
        });

        Navs.ForEach(nav =>
        {
            SameLevelNavs.Add(nav);
            if (nav.Children is not null) SameLevelNavs.AddRange(nav.Children);
        });

        SameLevelNavs.Where(nav => nav.Href is not null).ForEach(nav =>
        {
            // The following path will not open a new tab
            if (nav.Href is "app/user/view" or "app/user/edit" or "app/ecommerce/details")
            {
                nav.Target = "Self";
            }

            PageTabItems.Add(new(nav.Title, nav.Href, nav.ParentIcon));
        });
    }

    public void NavigateTo(NavModel nav)
    {
        _navigationManager.NavigateTo(nav.Href ?? "");
    }

    public void NavigateTo(string href)
    {
        _navigationManager.NavigateTo(href);
    }
}

public record PageTabItem(string Title, string Href, string Icon);
