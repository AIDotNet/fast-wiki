namespace FastWiki.Service.Service;

/// <summary>
/// 文件存储服务
/// </summary>
public sealed class StorageService : ApplicationService<StorageService>
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<UploadFileResult> UploadFile(HttpContext context)
    {
        var file = context.Request.Form.Files[0];
        var command = new UploadFileStorageCommand(file);

        await EventBus.PublishAsync(command);

        return new UploadFileResult
        {
            Id = command.Id,
            Path = command.Path
        };
    }
}