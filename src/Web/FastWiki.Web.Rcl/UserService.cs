using FastWiki.Infrastructure.Rcl.Command.JsInterop;
using Microsoft.AspNetCore.Components.Authorization;

namespace FastWiki.Web.Rcl;

public class UserService(LocalStorageJsInterop localStorageJsInterop, AuthenticationStateProvider authenticationStateProvider) : IUserService
{
    public async Task<string> GetTokenAsync()
    {
        return await localStorageJsInterop.GetLocalStorageAsync(Constant.Token);
    }

    public async Task LogoutAsync()
    {
        if (authenticationStateProvider is WikiAuthenticationStateProvider wikiAuthenticationStateProvider) 
        {
            wikiAuthenticationStateProvider.AuthenticateUser(string.Empty);

            await Task.CompletedTask;
        }
    }
}