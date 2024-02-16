using FastWiki.Service.Contracts.Storage.Dto;

namespace FastWiki.Service.Contracts.Storage;

public interface IStorageService
{
    Task<UploadFileResult> UploadFile(Stream stream,string name);
}