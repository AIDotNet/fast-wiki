using FastWiki.ApiGateway.caller.Service;
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

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();