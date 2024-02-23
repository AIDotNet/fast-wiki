using FastWiki.Service.Domain.Storage.Aggregates;

namespace FastWiki.Service.DataAccess.Repositories.Storage;

public class FileStorageRepository(WikiDbContext context, IUnitOfWork unitOfWork)
    : Repository<WikiDbContext, FileStorage, long>(context, unitOfWork), IFileStorageRepository
{
    public async Task<FileStorage> AddAsync(FileStorage fileStorage)
    {
        var entity = await Context.FileStorages.AddAsync(fileStorage);

        await Context.SaveChangesAsync();
        
        return entity.Entity;
    }
}