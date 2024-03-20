using FastWiki.Service.Domain.Model.Aggregates;

namespace FastWiki.Service.Domain.Model.Repositories;

public interface IFastModelRepository : IRepository<FastModel, string>
{
    /// <summary>
    /// 获取模型列表
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<List<FastModel>> GetModelListAsync(string keyword, int page, int pageSize);
    
    /// <summary>
    /// 获取模型数量
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    Task<long> GetModelCountAsync(string keyword);   
    
    /// <summary>
    /// 判断模型是否存在
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> ExistAsync(string name);

    /// <summary>
    /// 删除指定模型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(string id);
}