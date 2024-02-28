using Microsoft.IdentityModel.Tokens;

namespace FastWiki.Web.Rcl.Pages.Authentication;

public partial class Login
{
    private bool _show;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public bool HideLogo { get; set; }

    [Parameter]
    public double Width { get; set; } = 410;

    [Parameter]
    public StringNumber? Elevation { get; set; }

    [Parameter]
    public string CreateAccountRoute { get; set; } = $"pages/authentication/register-v1";

    [Parameter]
    public string ForgotPasswordRoute { get; set; } = $"pages/authentication/forgot-password-v1";

    private string account;
    private string pass;

    private async Task OnLogin()
    {
        try
        {

            var token = await AuthorizeService.TokenAsync(account, pass);

            if (!string.IsNullOrEmpty(token.Token))
            {
                ((WikiAuthenticationStateProvider)AuthenticationStateProvider)
                    .AuthenticateUser(token.Token);
                await LocalStorageJsInterop.SetLocalStorageAsync(Constant.Token, token.Token);

                Navigation.NavigateTo("/");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
