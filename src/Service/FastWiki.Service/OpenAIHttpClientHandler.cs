namespace FastWiki.Service;

/// <summary>
/// 重写SemanticKernel的请求处理
/// </summary>
public sealed class OpenAiHttpClientHandler : HttpClientHandler
{
    private readonly string _uri;

    public OpenAiHttpClientHandler()
    {
    }

    public OpenAiHttpClientHandler(string uri)
    {
        _uri = uri;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        UriBuilder uriBuilder;
        if (!OpenAIOption.ChatEndpoint.IsNullOrWhiteSpace() && request.RequestUri?.LocalPath == "/v1/chat/completions")
        {
            uriBuilder = _uri.IsNullOrWhiteSpace() ? new UriBuilder(OpenAIOption.ChatEndpoint.TrimEnd('/') + "/v1/chat/completions") : new UriBuilder(_uri.TrimEnd('/') + "/v1/chat/completions");
            request.RequestUri = uriBuilder.Uri;
        }
        else if (!OpenAIOption.EmbeddingEndpoint.IsNullOrWhiteSpace() &&
                 request.RequestUri?.LocalPath == "/v1/embeddings")
        {
            uriBuilder = _uri.IsNullOrWhiteSpace() ? new UriBuilder(OpenAIOption.EmbeddingEndpoint.TrimEnd('/') + "/v1/embeddings") : new UriBuilder(_uri.TrimEnd('/') + "/v1/embeddings");
            request.RequestUri = uriBuilder.Uri;
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}