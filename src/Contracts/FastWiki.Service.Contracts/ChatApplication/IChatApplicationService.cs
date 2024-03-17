using FastWiki.Service.Contracts.ChatApplication.Dto;

namespace FastWiki.Service.Contracts.ChatApplication;

public interface IChatApplicationService
{
    /// <summary>
    /// 创建聊天应用
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateAsync(CreateChatApplicationInput input);

    /// <summary>
    /// 删除聊天应用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(string id);

    /// <summary>
    /// 编辑聊天应用
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(UpdateChatApplicationInput input);

    /// <summary>
    /// 获取聊天应用列表
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PaginatedListBase<ChatApplicationDto>> GetListAsync(int page, int pageSize);

    /// <summary>
    /// 获取聊天应用详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ChatApplicationDto> GetAsync(string id);

    /// <summary>
    /// 使用分享对话id获取应用
    /// </summary>
    /// <param name="chatShareId"></param>
    /// <returns></returns>
    Task<ChatApplicationDto> GetChatShareApplicationAsync(string chatShareId);

    /// <summary>
    /// 创建对话
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateChatDialogAsync(CreateChatDialogInput input);

    /// <summary>
    /// 获取对话列表
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="all"></param>
    /// <returns></returns>
    Task<List<ChatDialogDto>> GetChatDialogAsync(string applicationId,bool all);

    /// <summary>
    /// 获取分享对话列表
    /// </summary>
    /// <param name="chatId"></param>
    /// <returns></returns>
    Task<List<ChatDialogDto>> GetChatShareDialogAsync(string chatId);
    
    /// <summary>
    /// 创建对话记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateChatDialogHistoryAsync(CreateChatDialogHistoryInput input);

    /// <summary>
    /// 获取对话记录
    /// </summary>
    /// <param name="chatDialogId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PaginatedListBase<ChatDialogHistoryDto>>
        GetChatDialogHistoryAsync(string chatDialogId, int page, int pageSize);

    /// <summary>
    /// 删除指定记录
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveDialogHistoryAsync(string id);

    /// <summary>
    /// 分享指定应用
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateShareAsync(CreateChatShareInput input);

    /// <summary>
    /// 获取对话列表
    /// </summary>
    /// <param name="chatApplicationId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PaginatedListBase<ChatShareDto>> GetChatShareListAsync(string chatApplicationId, int page, int pageSize);
    
    /// <summary>
    /// 删除分享
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveChatShareAsync(string id);

    /// <summary>
    /// 删除对话
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveDialogAsync(string id);

    /// <summary>
    /// 编辑对话
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateDialogAsync(ChatDialogDto input);

    /// <summary>
    /// 删除指定分享对话
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveShareDialogAsync(string chatId, string id);

    /// <summary>
    /// 编辑分享对话
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateShareDialogAsync(ChatDialogDto input);

    /// <summary>
    /// 获取对话记录列表
    /// </summary>
    /// <param name="chatApplicationId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PaginatedListBase<ChatDialogDto>> GetSessionLogDialogAsync(string chatApplicationId, int page, int pageSize);
    
    /// <summary>
    /// 修改对话记录内容
    /// </summary>
    /// <returns></returns>
    Task PutChatHistoryAsync(PutChatHistoryInput input);
}