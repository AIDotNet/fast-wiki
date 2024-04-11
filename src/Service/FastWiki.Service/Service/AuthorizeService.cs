using FastWiki.Service.Application.Users.Queries;
using FastWiki.Service.Contracts.Users;
using FastWiki.Service.Contracts.Users.Dto;
using FastWiki.Service.Infrastructure.Helper;

namespace FastWiki.Service.Service;

/// <summary>
/// 授权服务
/// </summary>
public sealed class AuthorizeService : ApplicationService<AuthorizeService>, IAuthorizeService
{
    public async Task<AuthorizeDto> TokenAsync(string account, string pass)
    {
        var userInfo = new UserInfoQuery(account, pass);

        await EventBus.PublishAsync(userInfo);

        return new AuthorizeDto
        {
            Token = JwtHelper.GeneratorAccessToken(userInfo.Result)
        };
    }
}