namespace FastWiki.Service.Contracts.Users;

public class AuthorizeInput
{
    public string Account { get; set; }

    public string Password { get; set; }
}