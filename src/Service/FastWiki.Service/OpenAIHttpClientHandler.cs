namespace FastWiki.Service;

/// <summary>
/// 重写SemanticKernel的请求处理
/// </summary>
public sealed class OpenAiHttpClientHandler : HttpClientHandler
{
    private readonly string _uri;

    public OpenAiHttpClientHandler(string uri)
    {
        _uri = uri;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.RequestUri =
            new Uri(request.RequestUri.ToString().Replace("https://api.openai.com", _uri.TrimEnd('/')));

        return await base.SendAsync(request, cancellationToken);
    }
}