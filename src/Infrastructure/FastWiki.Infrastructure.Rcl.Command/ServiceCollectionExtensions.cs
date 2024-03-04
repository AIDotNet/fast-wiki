using FastWiki.Infrastructure.Rcl.Command.JsInterop;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRclCommand(this IServiceCollection services)
    {
        services.AddScoped<JsHelperJsInterop>();
        services.AddScoped<LocalStorageJsInterop>();
        return services;
    }
}
