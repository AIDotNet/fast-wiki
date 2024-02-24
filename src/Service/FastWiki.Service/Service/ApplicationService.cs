namespace FastWiki.Service.Service;

public abstract class ApplicationService<TService> : ServiceBase where TService : class
{
    protected IEventBus EventBus => GetRequiredService<IEventBus>();

    protected ILogger<TService> Logger => GetRequiredService<ILogger<TService>>();

}