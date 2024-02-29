namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFastWikiApiGateways(this IServiceCollection services,
        Action<MasaHttpClient> clientBuilder, Action<HttpClient> httpClientBuilder)
    {
        services.AddScoped<IWikiService, WikiService>();
        services.AddScoped<IChatApplicationService, ChatApplicationService>();
        services.AddScoped<IAuthorizeService, AuthorizeService>();
        services.AddScoped<IStorageService, StorageService>();

        services.AddCaller(callerBuilder =>
        {
            callerBuilder.UseHttpClient(clientBuilder!.Invoke)
                .AddMiddleware<AuthorizeMiddleware>(ServiceLifetime.Scoped);
        });

        services.AddHttpClient(Constant.ApiGatewayHttpClient, httpClientBuilder.Invoke);

        return services;
    }
}