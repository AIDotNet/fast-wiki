using FastWiki.Service.Domain.Function.Aggregates;

namespace FastWiki.Service.Domain.Function.Repositories;

public interface IFastWikiFunctionCallRepository : IRepository<FastWikiFunctionCall, long>
{
    /// <summary>
    ///     获取function call列表
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<FastWikiFunctionCall>> GetFunctionListAsync(Guid? userId, int page, int pageSize);

    /// <summary>
    ///     获取function call详情
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<long> GetFunctionCountAsync(Guid? userId);

    /// <summary>
    ///     启用/禁用function call
    /// </summary>
    /// <param name="id"></param>
    /// <param name="enable"></param>
    /// <returns></returns>
    Task EnableFunctionCallAsync(long id, bool enable);

    /// <summary>
    ///     获取function call列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<FastWikiFunctionCall>> GetFunctionListAsync(Guid? userId);

    /// <summary>
    ///     删除function call
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task RemoveAsync(long id);
    
    /// <summary>
    /// 插入function call
    /// </summary>
    /// <param name="functionCall"></param>
    /// <returns></returns>
    Task InsertAsync(FastWikiFunctionCall functionCall);
}