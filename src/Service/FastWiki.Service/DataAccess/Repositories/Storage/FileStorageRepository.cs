using FastWiki.Service.Domain.Storage.Aggregates;
using FastWiki.Service.Domain.Storage.Repositories;

namespace FastWiki.Service.DataAccess.Repositories.Storage;

public class FileStorageRepository(WikiDbContext context, IUnitOfWork unitOfWork)
    : Repository<WikiDbContext, FileStorage, long>(context, unitOfWork), IFileStorageRepository
{
    
}