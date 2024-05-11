using FastWiki.Service.Contracts.Users.Dto;

namespace FastWiki.Service.Application.Users.Queries;

/// <summary>
/// 通过账号密码获取用户信息
/// </summary>
/// <param name="Account"></param>
/// <param name="Pass"></param>
public record UserInfoQuery(string Account,string Pass):Query<UserDto>
{
    public override UserDto Result { get; set; }
}