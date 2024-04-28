using FastWiki.Service.DataAccess;
using FastWiki.Service.Dto;
using FastWiki.Service.Infrastructure.Helper;
using FastWiki.Service.Input;
using Microsoft.EntityFrameworkCore;

namespace FastWiki.Service.Services;

/// <summary>
/// 授权服务
/// </summary>
public sealed class AuthorizeService(MasterDbContext masterDbContext) : ApplicationService<AuthorizeService>
{
    public async Task<AuthorizeDto> TokenAsync(AuthorizeInput input)
    {
        var user = await masterDbContext.Users.FirstOrDefaultAsync(x =>
            x.Account == input.Account && x.Password == input.Password);

        if (user == null)
        {
            throw new UserFriendlyException("账号或密码错误");
        }

        if (user.CheckCipher(input.Password))
        {
            throw new UserFriendlyException("账号或密码错误");
        }

        var token = JWTHelper.GeneratorAccessToken(Mapper.Map<UserDto>(user));

        return new AuthorizeDto
        {
            Token = token
        };
    }
}