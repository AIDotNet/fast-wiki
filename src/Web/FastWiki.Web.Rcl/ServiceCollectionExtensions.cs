using FastWiki.Web.Rcl.JsModules;

namespace FastWiki.Web.Rcl;

public static class ServiceCollectionExtensions
{
    public static void AddFastWikiWebRcl(this IServiceCollection services)
    {
        services.AddScoped<JsHelperModule>();
    }
}