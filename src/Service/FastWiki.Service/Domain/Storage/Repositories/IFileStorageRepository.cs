using FastWiki.Service.Domain.Storage.Aggregates;

namespace FastWiki.Service.Domain.Storage.Repositories;

public interface IFileStorageRepository : IRepository<FileStorage, long>
{
    Task<FileStorage> AddAsync(FileStorage fileStorage);
}