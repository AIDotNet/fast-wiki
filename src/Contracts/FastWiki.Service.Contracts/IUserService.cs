namespace FastWiki.Service.Contracts;

public interface IUserService
{
    Task<string> GetTokenAsync();
}