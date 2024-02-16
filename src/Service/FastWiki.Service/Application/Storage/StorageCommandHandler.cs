using FastWiki.Service.Domain.Storage.Aggregates;

namespace FastWiki.Service.Application.Storage;

/// <summary>
/// 存储命令处理器
/// </summary>
/// <param name="fileStorageRepository"></param>
public class StorageCommandHandler(IFileStorageRepository fileStorageRepository)
{
    [EventHandler]
    public async Task UploadFileStorage(UploadFileStorageCommand command)
    {
        var filePath = "uploads/" + Guid.NewGuid().ToString("N") + Path.GetExtension(command.File.FileName);
        var fileStreamPath = Path.Combine("wwwroot", filePath);

        var fileInfo = new FileInfo(fileStreamPath);

        if (fileInfo.Directory?.Exists == false)
        {
            fileInfo.Directory.Create();
        }

        await using var fileStream = fileInfo.Create();
        await command.File.CopyToAsync(fileStream);

        var fileStorage = new FileStorage(command.File.FileName, "/" + filePath, command.File.Length, false);

        fileStorage = await fileStorageRepository.AddAsync(fileStorage);

        command.Path = fileStorage.Path;
        command.Id = fileStorage.Id;
    }
}