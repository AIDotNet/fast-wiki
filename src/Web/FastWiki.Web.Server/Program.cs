using FastWiki.Web.Rcl;
using Microsoft.AspNetCore.Components.Authorization;


var FAST_WIKI_SERVICE = Environment.GetEnvironmentVariable("FAST_WIKI_SERVICE");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services
    .AddMasaBlazor(builder =>
    {
        builder.ConfigureTheme(theme =>
        {
            theme.Themes.Light.Primary = "#4318FF";
            theme.Themes.Light.Accent = "#4318FF";
        });
    }).AddI18nForServer(Path.Combine("wwwroot", "i18n"));

builder.Services.AddCascadingAuthenticationState()
    .AddRclCommand();

builder.Services.AddMasaIdentity();

builder.Services.AddScoped<AuthenticationStateProvider,
    WikiAuthenticationStateProvider>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

if (FAST_WIKI_SERVICE.IsNullOrWhiteSpace())
{
    FAST_WIKI_SERVICE = "http://localhost:5124";
}

if (!FAST_WIKI_SERVICE.StartsWith("http://") && !FAST_WIKI_SERVICE.StartsWith("https://"))
{
    FAST_WIKI_SERVICE = "http://" + FAST_WIKI_SERVICE;
}

builder.Services.AddFastWikiApiGateways(options =>
{
    options.BaseAddress = FAST_WIKI_SERVICE.TrimEnd('/');
    options.Prefix = "/api/v1/";
}, options => { options.BaseAddress = new Uri(FAST_WIKI_SERVICE.TrimEnd('/') + "/api/v1/"); }).AddFastWikiWebRcl();

builder.Services.AddGlobalForServer();

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();