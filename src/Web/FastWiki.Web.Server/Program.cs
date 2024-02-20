using FastWiki.Web.Rcl;

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

builder.Services.AddFastWikiApiGateways(options =>
{
    options.BaseAddress = "http://localhost:5124";
    options.Prefix = "/api/v1/";
});
builder.Services.AddFastWikiWebRcl();

builder.Services.AddGlobalForServer();

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();