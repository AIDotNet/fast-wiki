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
    Task CreateChatDialogAsync(ChatDialog chatDialog);

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

    /// <summary>
    /// 创建对话记录
    /// </summary>
    /// <returns></returns>
    Task CreateChatDialogHistoryAsync(ChatDialogHistory chatDialogHistory);

    /// <summary>
    /// 获取对话记录
    /// </summary>
    /// <param name="chatDialogId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<ChatDialogHistory>> GetChatDialogHistoryListAsync(string chatDialogId, int page, int pageSize);

    /// <summary>
    /// 获取对话记录数量
    /// </summary>
    /// <param name="chatDialogId"></param>
    /// <returns></returns>
    Task<long> GetChatDialogHistoryCountAsync(string chatDialogId);

    /// <summary>
    /// 删除对话记录数量
    /// </summary>
    /// <param name="chatDialogId"></param>
    /// <returns></returns>
    Task RemoveChatDialogHistoryAsync(string chatDialogId);

    /// <summary>
    /// 删除指定id的数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveChatDialogHistoryByIdAsync(string id);
}