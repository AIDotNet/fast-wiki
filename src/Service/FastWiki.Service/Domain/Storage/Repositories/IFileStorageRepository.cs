using FastWiki.Service.Domain.Storage.Aggregates;

namespace FastWiki.Service.Domain.Storage.Repositories;

public interface IFileStorageRepository : IRepository<FileStorage, long>
{
    Task<FileStorage> AddAsync(FileStorage fileStorage);
    
    /// <summary>
    /// 获取文件存储列表
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<List<FileStorage>> GetListAsync(params long[] ids);
}