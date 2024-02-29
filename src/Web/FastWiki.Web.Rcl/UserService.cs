using FastWiki.Infrastructure.Rcl.Command.JsInterop;

namespace FastWiki.Web.Rcl;

public class UserService(LocalStorageJsInterop localStorageJsInterop) : IUserService
{
    public async Task<string> GetTokenAsync()
    {
        return await localStorageJsInterop.GetLocalStorageAsync(Constant.Token);
    }
}