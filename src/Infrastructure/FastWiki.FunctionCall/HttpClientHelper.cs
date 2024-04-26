using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FastWiki.FunctionCall;

public class HttpClientHelper
{
    private static readonly HttpClient Client = new ();

    static HttpClientHelper()
    {
        Client.DefaultRequestHeaders.Add("User-Agent", "FastWiki.FunctionCall");
    }

    public static async Task<string> GetAsync(string uri)
    {
        using var response = await Client.GetAsync(uri);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> PostAsync(string uri, object data)
    {
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        using var response = await Client.PostAsync(uri, content);
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> PutAsync(string uri, object data)
    {
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        using var response = await Client.PutAsync(uri, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public static async Task<string> DeleteAsync(string uri)
    {
        using var response = await Client.DeleteAsync(uri);
        return await response.Content.ReadAsStringAsync();
    }
}