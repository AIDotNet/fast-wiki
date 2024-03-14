using FastWiki.Service.Application.Users.Commands;

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
}