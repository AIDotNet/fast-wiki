using System.ComponentModel;
using FastWiki.Service.Contracts.Storage.Dto;

namespace FastWiki.Service.Service;

public sealed class StorageService : ApplicationService<StorageService>
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [Description("上传文件")]
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