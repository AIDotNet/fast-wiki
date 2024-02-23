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
}