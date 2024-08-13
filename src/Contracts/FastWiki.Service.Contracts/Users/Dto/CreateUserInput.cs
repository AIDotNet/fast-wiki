namespace FastWiki.Service.Contracts.Users.Dto;

public class CreateUserInput
{
    /// <summary>
    ///     ÕË»§
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    ///     êÇ³Æ
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     ÃÜÂë
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     ÓÊÏä
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     ÊÖ»úºÅ
    /// </summary>
    public string Phone { get; set; }
}