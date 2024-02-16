namespace FastWiki.ApiGateway.Caller;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFastWikiApiGateways(this IServiceCollection services,
        Action<MasaHttpClient>? clientBuilder)
    {
        services.AddScoped<IWikiService, WikiService>();
        services.AddScoped<IStorageService, StorageService>();
        
        services.AddCaller(callerBuilder =>
        {
            callerBuilder.UseHttpClient(httpClient => { clientBuilder?.Invoke(httpClient); });
        });

        return services;
    }
}