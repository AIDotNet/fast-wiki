using FastWiki.Service.Application.Users.Commands;
using FastWiki.Service.Application.Users.Queries;
using FastWiki.Service.Contracts.Users.Dto;

namespace FastWiki.Service.Service;

public sealed class UserService : ApplicationService<UserService>
{
    [Authorize]
    public async Task<ResultDto> ChangePasswordAsync(string password, string newPassword)
    {
        var command = new ChangePasswordCommand(UserContext.GetUserId<Guid>(), password, newPassword);

        await EventBus.PublishAsync(command);

        return new ResultDto();
    }

    [Authorize(Roles = Constant.Role.Admin)]
    public async Task<PaginatedListBase<UserDto>> GetUsersAsync(string? keyword, int page, int pageSize)
    {
        var query = new UserListQuery(keyword, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }

    [Authorize(Roles = Constant.Role.Admin)]
    public async Task DeleteUserAsync(Guid id)
    {
        if (id == UserContext.GetUserId<Guid>())
            throw new UserFriendlyException("不能删除自己");

        var command = new DeleteUserCommand(id);

        await EventBus.PublishAsync(command);
    }

    [Authorize(Roles = Constant.Role.Admin)]
    public async Task DisableUserAsync(Guid id, bool disable)
    {
        if (id == UserContext.GetUserId<Guid>())
            throw new UserFriendlyException("不能禁用自己");

        var command = new DisableUserCommand(id, disable);

        await EventBus.PublishAsync(command);
    }

    [Authorize(Roles = Constant.Role.Admin)]
    public async Task UpdateRoleAsync(Guid id, RoleType role)
    {
        var command = new UpdateRoleCommand(id, role);

        await EventBus.PublishAsync(command);
    }
}