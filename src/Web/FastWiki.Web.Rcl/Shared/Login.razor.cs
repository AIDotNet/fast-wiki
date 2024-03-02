using FastWiki.Web.Rcl.Data.Base;

namespace FastWiki.Web.Rcl.Shared;

public partial class Login
{
    private async Task OnLogout()
    {
        await LocalStorageJsInterop.RemoveLocalStorageAsync(Constant.Token);
        NavigationManager.NavigateTo(GlobalVariables.OnLogoutRoute);
    }
}
