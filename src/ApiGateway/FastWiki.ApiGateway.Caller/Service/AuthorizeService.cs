using FastWiki.ApiGateway.caller.Service;
using FastWiki.Service.Contracts.Users.Dto;
using System.Net.Http.Json;

namespace FastWiki.ApiGateway.Caller.Service;

public sealed class AuthorizeService(ICaller caller, IHttpClientFactory httpClientFactory)
    : ServiceBase(caller, httpClientFactory), IAuthorizeService
{
    protected override string BaseUrl { get; set; } = "Authorizes";

    public async Task<AuthorizeDto> TokenAsync(string account, string pass)
    {
        var response = await SendAsync(HttpMethod.Post,
            nameof(TokenAsync) + $"?{nameof(account)}={account}&{nameof(pass)}={pass}", null,
            HttpCompletionOption.ResponseContentRead);

        return await response.Content.ReadFromJsonAsync<AuthorizeDto>();
    }
}