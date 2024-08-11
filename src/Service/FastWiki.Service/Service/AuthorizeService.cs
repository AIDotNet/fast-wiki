using FastWiki.Service.Application.Users.Queries;
using FastWiki.Service.Contracts.Users;
using FastWiki.Service.Contracts.Users.Dto;
using FastWiki.Service.Domain.Users.Repositories;
using FastWiki.Service.Infrastructure.Helper;

namespace FastWiki.Service.Service;

/// <summary>
/// 授权服务
/// </summary>
public sealed class AuthorizeService(IUserRepository userRepository, IMapper mapper)
    : ApplicationService<AuthorizeService>, IAuthorizeService
{
    public async Task<AuthorizeDto> TokenAsync(AuthorizeInput input)
    {
        var dto = await userRepository.FindAsync(x => x.Account == input.Account);

        if (dto == null)
        {
            throw new UserFriendlyException("账号不存在");
        }

        if (!dto.CheckCipher(input.Password))
        {
            throw new UserFriendlyException("密码错误");
        }

        if (dto.IsDisable)
        {
            throw new UserFriendlyException("账号已禁用");
        }


        return new AuthorizeDto
        {
            Token = JwtHelper.GeneratorAccessToken(mapper.Map<UserDto>(dto))
        };
    }
}