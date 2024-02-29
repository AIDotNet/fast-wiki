using FastWiki.ApiGateway.caller.Service;
using FastWiki.Service.Contracts.Storage.Dto;
using System.Net.Http.Json;

namespace FastWiki.ApiGateway.Caller.Service;

public sealed class StorageService(ICaller caller, IHttpClientFactory httpClientFactory,IUserService userService)
    : ServiceBase(caller, httpClientFactory,userService), IStorageService
{
    protected override string BaseUrl { get; set; } = "Storages";

    public async Task<UploadFileResult> UploadFile(Stream stream, string name)
    {
        var multipartContent = new MultipartFormDataContent
        {
            { new StreamContent(stream), "file", name }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/" + BaseUrl + "/UploadFile");

        request.Content = multipartContent;

        var response = await caller.SendAsync(request);

        return await response.Content.ReadFromJsonAsync<UploadFileResult>();
    }
}