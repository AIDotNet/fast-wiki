using FastWiki.Service.Domain.Storage.Aggregates;

namespace FastWiki.Service.Service;

/// <summary>
///     文件存储服务
/// </summary>
public sealed class StorageService(IFileStorageRepository fileStorageRepository) : ApplicationService<StorageService>
{
    /// <summary>
    ///     上传文件
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<UploadFileResult> UploadFile(HttpContext context)
    {
        var file = context.Request.Form.Files[0];

        var filePath = "uploads/" + DateTime.Now.ToString("yyyyMMdd") + "/" + Guid.NewGuid().ToString("N") +
                       file.FileName;
        var fileStreamPath = Path.Combine("wwwroot", filePath);

        var fileInfo = new FileInfo(fileStreamPath);

        if (fileInfo.Directory?.Exists == false) fileInfo.Directory.Create();

        await using var fileStream = fileInfo.Create();

        await file.CopyToAsync(fileStream);

        var fileStorage = new FileStorage(file.FileName, OpenAIOption.Site.TrimEnd("/") + "/" + filePath,
            file.Length, false);

        fileStorage.SetFullName(fileInfo.FullName);

        fileStorage = await fileStorageRepository.AddAsync(fileStorage);

        return new UploadFileResult
        {
            Id = fileStorage.Id,
            Path = fileStorage.Path
        };
    }
}