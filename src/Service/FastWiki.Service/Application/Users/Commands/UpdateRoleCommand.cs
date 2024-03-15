namespace FastWiki.Service.Application.Users.Commands;

/// <summary>
/// 修改用户角色命令
/// </summary>
/// <param name="Id"></param>
/// <param name="Role"></param>
public record UpdateRoleCommand(Guid Id, RoleType Role):Command;