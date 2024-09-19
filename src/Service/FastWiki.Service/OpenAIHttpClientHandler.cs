using System.Text;
using System.Text.Json;
using Serilog;

namespace FastWiki.Service;

/// <summary>
///     重写SemanticKernel的请求处理
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
        var mediaType = request.Content.Headers.ContentType.MediaType;

        request.Content = new StringContent(Unescape(await request.Content.ReadAsByteArrayAsync(cancellationToken)),
            Encoding.UTF8, mediaType);

        request.RequestUri =
            new Uri(request.RequestUri.ToString().Replace("https://api.openai.com/v1", _uri.TrimEnd('/')));

        return await base.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// 请求的编码可能和导致AI智商降低，所以我们转移成UTF8
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static string Unescape(byte[] stream)
    {
        try
        {
            var str = JsonSerializer.Serialize(JsonSerializer.Deserialize(stream, typeof(object),
                new JsonSerializerOptions()
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                }), new JsonSerializerOptions()
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            });
            return str;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return "";
        }
    }
}