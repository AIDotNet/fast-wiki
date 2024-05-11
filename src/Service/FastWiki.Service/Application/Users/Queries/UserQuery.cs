namespace FastWiki.Service.Application.Users.Queries;

/// <summary>
/// 获取用户信息
/// </summary>
/// <param name="Id"></param>
public record UserQuery(Guid Id):Query<User?>
{
    public override User? Result { get; set; }
}