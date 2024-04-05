using System.Text;
using System.Text.Json;
using FastWiki.Service.Contracts.OpenAI;

namespace FastWiki.Service.Infrastructure;

public static class HttpContextExtension
{
    
    public static async ValueTask WriteOpenAiResultAsync(this HttpContext context, string content, string model,
        string systemFingerprint, string id)
    {
        var openAiResult = new OpenAIResult()
        {
            id = id,
            _object = "chat.completion.chunk",
            created = DateTimeOffset.Now.ToUnixTimeSeconds(),
            model = model,
            system_fingerprint = systemFingerprint,
            choices =
            [
                new Choice
                {
                    index = 0,
                    delta = new MessageDto()
                    {
                        content = content,
                        role = "assistant"
                    },
                    finish_reason = null
                }
            ]
        };

        await context.Response.WriteAsync("data: " + JsonSerializer.Serialize(openAiResult, new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        }) + "\n\n", Encoding.UTF8);
        await context.Response.Body.FlushAsync();
    }

    public static async Task WriteEndAsync(this HttpContext context, string content)
    {
        await context.Response.WriteAsync("data: " + JsonSerializer.Serialize(new OpenAIResult()
        {
            id = Guid.NewGuid().ToString("N"),
            _object = "chat.completion.chunk",
            created = DateTimeOffset.Now.ToUnixTimeSeconds(),
            model = string.Empty,
            system_fingerprint = Guid.NewGuid().ToString("N"),
            choices =
            [
                new Choice
                {
                    index = 0,
                    delta = new MessageDto()
                    {
                        content = content,
                        role = "assistant"
                    },
                    finish_reason = null
                }
            ]
        }, new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        }) + "\n\n", Encoding.UTF8);
        await context.Response.Body.FlushAsync();
        await context.WriteEndAsync();
    }

    /// <summary>
    /// 输出结束
    /// </summary>
    /// <param name="context"></param>
    public static async Task WriteEndAsync(this HttpContext context)
    {
        await context.Response.WriteAsync("data: [DONE]\n\n");
        await context.Response.Body.FlushAsync();
    }
}