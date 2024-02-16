using Masa.BuildingBlocks.Service.Caller;
using Masa.Contrib.Service.Caller.HttpClient;

namespace FastWiki.ApiGateway.Caller;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFastWikiApiGateways(this IServiceCollection services,
        Action<MasaHttpClient>? clientBuilder)
    {
        services.AddCaller(callerBuilder =>
        {
            callerBuilder.UseHttpClient(httpClient => { clientBuilder?.Invoke(httpClient); });
        });

        return services;
    }
}