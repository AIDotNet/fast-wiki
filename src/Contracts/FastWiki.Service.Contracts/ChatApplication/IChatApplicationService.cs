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
    /// 创建对话
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateChatDialogAsync(CreateChatDialogInput input);

    /// <summary>
    /// 获取对话列表
    /// </summary>
    /// <returns></returns>
    Task<List<ChatDialogDto>> GetChatDialogAsync();

    /// <summary>
    /// 智能对话
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    IAsyncEnumerable<CompletionsDto> CompletionsAsync(CompletionsInput input);
}