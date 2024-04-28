namespace FastWiki.Service.Input;

public class CreateUserInput
{
    /// <summary>
    /// 账户
    /// </summary>
    public string Account { get;  set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Name { get;  set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get;  set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get;  set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get;  set; }
}