using FastWiki.Service.Contracts;
using FastWiki.Service.Contracts.ChatApplication;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFastWikiApiGateways(this IServiceCollection services,
        Action<MasaHttpClient> clientBuilder, Action<HttpClient> httpClientBuilder)
    {
        services.AddScoped<IWikiService, WikiService>();
        services.AddScoped<IChatApplicationService, ChatApplicationService>();
        services.AddScoped<IStorageService, StorageService>();

        services.AddCaller(callerBuilder =>
        {
            callerBuilder.UseHttpClient(clientBuilder!.Invoke);
        });

        services.AddHttpClient(Constant.ApiGatewayHttpClient, httpClientBuilder.Invoke);

        return services;
    }
}