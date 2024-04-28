using MapsterMapper;
using Masa.BuildingBlocks.Authentication.Identity;

namespace FastWiki.Service.Services;

public abstract class ApplicationService<TService> : ServiceBase where TService : class
{
    protected ILogger<TService> Logger => GetRequiredService<ILogger<TService>>();

    protected IUserContext UserContext => GetRequiredService<IUserContext>();
    
    protected IMapper Mapper => GetRequiredService<IMapper>();
}