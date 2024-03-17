using System.Text.RegularExpressions;
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

    [EventHandler]
    public async Task CreateUserAsync(CreateUserCommand command)
    {
        // 校验账号和密码长度
        if (command.Input.Account.Length < 6 || command.Input.Account.Length > 20)
            throw new UserFriendlyException("账号长度必须在6-20之间");
        
        if (command.Input.Password.Length < 6 || command.Input.Password.Length > 20)
            throw new UserFriendlyException("密码长度必须在6-20之间");
        
        // 校验邮箱格式
        if (!Regex.IsMatch(command.Input.Email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$"))
            throw new UserFriendlyException("邮箱格式错误");
        
        // TODO: 验证账号是否存在
        if(await userRepository.IsExistAccountAsync(command.Input.Account))
            throw new UserFriendlyException("账号已存在");
        
        var user = new User(command.Input.Account, command.Input.Name, command.Input.Password,
            "https://blog-simple.oss-cn-shenzhen.aliyuncs.com/Avatar.jpg", command.Input.Email, command.Input.Phone,
            false);
        await userRepository.AddAsync(user);
    }
}