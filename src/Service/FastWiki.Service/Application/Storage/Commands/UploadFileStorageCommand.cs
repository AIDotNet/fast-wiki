namespace FastWiki.Service.Application.Storage.Commands;

/// <summary>
/// 上传文件存储命令
/// </summary>
/// <param name="File"></param>
public record UploadFileStorageCommand(IFormFile File) : Command
{
    /// <summary>
    /// 上传返回文件地址
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 上传返回文件Id
    /// </summary>
    public long Id { get; set; }
}