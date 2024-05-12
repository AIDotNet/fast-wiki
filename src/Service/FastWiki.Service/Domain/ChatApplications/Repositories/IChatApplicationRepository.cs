using Masa.BuildingBlocks.Data.Mapping.Options;

namespace FastWiki.Service.Domain.ChatApplications.Repositories;

public interface IChatApplicationRepository : IRepository<ChatApplication, string>
{
    Task<List<ChatApplication>> GetListAsync(int page, int pageSize, Guid userId);

    Task<long> GetCountAsync(Guid queryUserId);

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
    /// 通过APIKey获取分享对话
    /// </summary>
    /// <param name="apiKey"></param>
    /// <returns></returns>
    Task<ChatShare> GetAPIKeyChatShareAsync(string apiKey);

    /// <summary>
    /// 扣款指定分享对话的token
    /// </summary>
    /// <param name="chatShareId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task DeductTokenAsync(string chatShareId, int token);

    /// <summary>
    /// 删除分享
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveChatShareAsync(string id);
    
    /// <summary>
    /// 创建对话记录
    /// </summary>
    /// <param name="chatRecord"></param>
    /// <returns></returns>
    Task CreateChatRecordAsync(ChatRecord chatRecord);
    
    /// <summary>
    /// 创建问题
    /// </summary>
    /// <param name="questions"></param>
    /// <returns></returns>
    Task CreateQuestionsAsync(Questions questions);

    /// <summary>
    /// 移除问题
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveQuestionsAsync(string id);

    /// <summary>
    /// 获取问题列表
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    Task<List<Questions>> GetQuestionsAsync(string applicationId);
}