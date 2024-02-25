namespace FastWiki.Service.Domain.ChatApplications.Repositories;

public interface IChatApplicationRepository : IRepository<ChatApplication, string>
{
    Task<List<ChatApplication>> GetListAsync(int page, int pageSize);

    Task<long> GetCountAsync();

    /// <summary>
    /// 创建对话
    /// </summary>
    /// <param name="chatDialog"></param>
    /// <returns></returns>
    Task CreateChatDialogAsync(ChatDialog  chatDialog);
}