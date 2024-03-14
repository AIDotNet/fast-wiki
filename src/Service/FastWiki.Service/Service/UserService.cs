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
    public async Task<PaginatedListBase<UserDto>> GetUsersAsync(string keyword, int page, int pageSize)
    {
        var query = new UserListQuery(keyword, page, pageSize);

        await EventBus.PublishAsync(query);

        return query.Result;
    }
}