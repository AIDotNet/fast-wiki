using FastWiki.Infrastructure.Common.Helper;
using System.Text.RegularExpressions;

namespace FastWiki.Service.Domain.Users.Aggregates;

public sealed class User : FullAggregateRoot<Guid, Guid?>
{
    /// <summary>
    /// 账户
    /// </summary>
    public string Account { get; private set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// 密码盐
    /// </summary>
    public string Salt { get; private set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; private set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; private set; }

    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool IsDisable { get; private set; }

    /// <summary>
    /// 用户角色
    /// </summary>
    public RoleType Role { get; private set; }

    protected User()
    {
    }

    public User(string account, string name, string password, string avatar, string email, string phone,
        bool isDisable, RoleType role = RoleType.User)
    {
        Id = Guid.NewGuid();
        SetPassword(password);
        SetEmail(email);
        Account = account;
        Name = name;
        Avatar = avatar;
        Phone = phone;
        IsDisable = isDisable;
        Role = role;
    }

    public void Disable()
    {
        IsDisable = true;
    }

    public void Enable()
    {
        IsDisable = false;
    }

    public void SetAdminRole()
    {
        Role = RoleType.Admin;
    }

    public void SetUserRole()
    {
        Role = RoleType.User;
    }

    public void SetGuestRole()
    {
        Role = RoleType.Guest;
    }

    public void SetPassword(string password)
    {
        Salt = Guid.NewGuid().ToString("N");
        Password = Md5Helper.HashPassword(password, Salt);
    }

    public void SetEmail(string email)
    {
        // 使用正则表达式验证邮箱
        var regex = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        if (!regex.IsMatch(email))
        {
            throw new ArgumentException("邮箱格式不正确");
        }

        Email = email;
    }

    /// <summary>
    /// 校验密码
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool CheckCipher(string password)
    {
        if (Password == Md5Helper.HashPassword(password, Salt))
        {
            return true;
        }

        return false;
    }
}