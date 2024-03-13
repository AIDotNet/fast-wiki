using FastWiki.Service.Domain.Storage.Aggregates;

namespace FastWiki.Service.DataAccess.Repositories.Storage;

public sealed class FileStorageRepository(WikiDbContext context, IUnitOfWork unitOfWork)
    : Repository<WikiDbContext, FileStorage, long>(context, unitOfWork), IFileStorageRepository
{
    public async Task<FileStorage> AddAsync(FileStorage fileStorage)
    {
        var entity = await Context.FileStorages.AddAsync(fileStorage);

        await Context.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<List<FileStorage>> GetListAsync(params long[] ids)
    {
        return await Context.FileStorages.AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }
}