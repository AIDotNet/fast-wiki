using FastWiki.Service.Contracts.Users.Dto;

namespace FastWiki.Service.Contracts.Users;

public interface IAuthorizeService
{
    /// <summary>
    /// 登录获取token
    /// </summary>
    /// <returns></returns>
    Task<AuthorizeDto> TokenAsync(AuthorizeInput input);
}