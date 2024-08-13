using Masa.BuildingBlocks.Authentication.Identity;

namespace FastWiki.Service.Service;

public abstract class ApplicationService<TService> : ServiceBase where TService : class
{
    protected IEventBus EventBus => GetRequiredService<IEventBus>();

    protected ILogger<TService> Logger => GetRequiredService<ILogger<TService>>();

    protected IUserContext UserContext => GetRequiredService<IUserContext>();

    protected IMapper Mapper => GetRequiredService<IMapper>();
}