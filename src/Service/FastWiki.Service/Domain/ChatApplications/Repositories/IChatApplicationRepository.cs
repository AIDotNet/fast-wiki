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

    /// <summary>
    /// 删除对话
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveChatDialogAsync(string id);

    /// <summary>
    /// 获取对话列表
    /// </summary>
    /// <returns></returns>
    Task<List<ChatDialog>> GetChatDialogListAsync();
}