using FastWiki.Service.Application.Storage.Queries;

namespace FastWiki.Service.Application.Storage;

public class StorageQueryHandler(IFileStorageRepository fileStorageRepository)
{
    [EventHandler]
    public async Task StorageInfoAsync(StorageInfoQuery query)
    {
        var result = await fileStorageRepository.FindAsync(query.FileId);

        query.Result = result;
    }

    [EventHandler]
    public async Task StorageInfosAsync(StorageInfosQuery query)
    {
        var result = await fileStorageRepository.GetListAsync(x => query.FileId.Contains(x.Id));

        query.Result = result;
    }
}