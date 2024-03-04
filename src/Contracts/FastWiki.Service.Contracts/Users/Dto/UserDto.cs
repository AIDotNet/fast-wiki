namespace FastWiki.Service.Contracts.Users.Dto;

public sealed class UserDto
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// ÕË»§
    /// </summary>
    public string Account { get;  set; }

    /// <summary>
    /// êÇ³Æ
    /// </summary>
    public string Name { get;  set; }

    /// <summary>
    /// ÃÜÂë
    /// </summary>
    public string Password { get;  set; }

    /// <summary>
    /// ÃÜÂëÑÎ
    /// </summary>
    public string Salt { get;  set; }

    /// <summary>
    /// Í·Ïñ
    /// </summary>
    public string Avatar { get;  set; }

    /// <summary>
    /// ÓÊÏä
    /// </summary>
    public string Email { get;  set; }

    /// <summary>
    /// ÊÖ»úºÅ
    /// </summary>
    public string Phone { get;  set; }

    /// <summary>
    /// ÊÇ·ñ½ûÓÃ
    /// </summary>
    public bool IsDisable { get;  set; }
}