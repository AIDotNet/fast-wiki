using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using FastWiki.Infrastructure.Rcl.Command.JsInterop;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;

namespace FastWiki.Web.Rcl;

public class WikiAuthenticationStateProvider(LocalStorageJsInterop localStorageJsInterop) : AuthenticationStateProvider, IHostEnvironmentAuthenticationStateProvider
{

    private Task<AuthenticationState> _authenticationStateTask;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        var token = await localStorageJsInterop.GetLocalStorageAsync(Constant.Token);

        if (token.IsNullOrEmpty())
        {
            return new AuthenticationState(new ClaimsPrincipal(new NotIIdentity()));
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwtSecurityToken;

        try
        {
            jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        }
        catch (ArgumentException)
        {
            return new AuthenticationState(new ClaimsPrincipal(new NotIIdentity()));
        }

        var identity = new ClaimsIdentity(jwtSecurityToken.Claims, "Wiki-Authentication");

        var user = new ClaimsPrincipal(identity);


        return new AuthenticationState(user);
    }

    public void AuthenticateUser(string token)
    {
        if (token.IsNullOrEmpty())
        {
            return;
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwtSecurityToken;

        try
        {
            jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        }
        catch (ArgumentException)
        {
            return;
        }

        var identity = new ClaimsIdentity(jwtSecurityToken.Claims, "Wiki-Authentication");

        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(user)));
    }

    public void SetAuthenticationState(Task<AuthenticationState> authenticationStateTask)
    {
        _authenticationStateTask = authenticationStateTask ?? throw new ArgumentNullException(nameof(authenticationStateTask));
        NotifyAuthenticationStateChanged(_authenticationStateTask);
    }
}

public class NotIIdentity : IIdentity
{
    public string? AuthenticationType { get; }
    public bool IsAuthenticated { get; } = false;
    public string? Name { get; }
}