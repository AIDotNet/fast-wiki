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
    /// <param name="applicationId"></param>
    /// <param name="all"></param>
    /// <returns></returns>
    Task<List<ChatDialog>> GetChatDialogListAsync(string applicationId, bool all);
    
    /// <summary>
    /// 获取分享对话列表
    /// </summary>
    /// <param name="chatId"></param>
    /// <returns></returns>
    Task<List<ChatDialog>> GetChatShareDialogListAsync(string chatId);

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

    /// <summary>
    /// 创建分享对话
    /// </summary>
    /// <param name="share"></param>
    /// <returns></returns>
    Task CreateChatShareAsync(ChatShare share);

    /// <summary>
    /// 获取分享对话列表
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="chatApplicationId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<ChatShare>> GetChatShareListAsync(Guid userId, string chatApplicationId, int page, int pageSize);

    /// <summary>
    /// 获取分享对话数量
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="chatApplicationId"></param>
    /// <returns></returns>
    Task<long> GetChatShareCountAsync(Guid userId, string chatApplicationId);

    /// <summary>
    /// 获取分享对话
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ChatShare> GetChatShareAsync(string id);

    /// <summary>
    /// 通过分享对话获取应用
    /// </summary>
    /// <param name="chatShareId"></param>
    /// <returns></returns>
    Task<ChatApplication> ChatShareApplicationAsync(string chatShareId);

    /// <summary>
    /// 编辑对话
    /// </summary>
    /// <param name="chatDialog"></param>
    /// <returns></returns>
    Task UpdateChatDialogAsync(ChatDialog chatDialog);

    /// <summary>
    /// 删除分享对话
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveShareDialogAsync(string chatId, string id);

    /// <summary>
    /// 编辑分享对话
    /// </summary>
    /// <param name="chatDialog"></param>
    /// <returns></returns>
    Task UpdateShareDialogAsync(ChatDialog chatDialog);

    /// <summary>
    /// 获取对话记录的对话列表
    /// </summary>
    /// <param name="chatApplicationId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<ChatDialog>> GetSessionLogDialogListAsync(string chatApplicationId, int page, int pageSize);

    /// <summary>
    /// 获取对话记录的数量
    /// </summary>
    /// <param name="chatApplicationId"></param>
    /// <returns></returns>
    Task<long> GetSessionLogDialogCountAsync(string chatApplicationId);
}