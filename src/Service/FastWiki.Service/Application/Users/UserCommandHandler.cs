using FastWiki.Service.Application.Users.Commands;
using FastWiki.Service.Domain.Users.Repositories;

namespace FastWiki.Service.Application.Users;

public sealed class UserCommandHandler(IUserRepository userRepository)
{
    [EventHandler]
    public async Task ChangePasswordAsync(ChangePasswordCommand command)
    {
        var user = await userRepository.FindAsync(command.Id);

        if (user == null)
        {
            throw new UserFriendlyException("用户不存在");
        }

        if (!user.CheckCipher(command.Password))
        {
            throw new UserFriendlyException("密码错误");
        }

        user.SetPassword(command.NewPassword);

        await userRepository.UpdateAsync(user);
    }

    [EventHandler]
    public async Task DeleteUserAsync(DeleteUserCommand command)
    {
        await userRepository.DeleteAsync(command.Id);
    }

    [EventHandler]
    public async Task DisableUserAsync(DisableUserCommand command)
    {
        await userRepository.DisableAsync(command.Id, command.IsDisable);
    }

    [EventHandler]
    public async Task UpdateRoleAsync(UpdateRoleCommand command)
    {
        await userRepository.UpdateRoleAsync(command.Id, command.Role);
    }
}