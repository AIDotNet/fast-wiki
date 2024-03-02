namespace FastWiki.Service.Contracts;

public interface IUserService
{
    Task<string> GetTokenAsync();

    /// <summary>
    /// ÍË³öµÇÂ¼
    /// </summary>
    /// <returns></returns>
    Task LogoutAsync();
}