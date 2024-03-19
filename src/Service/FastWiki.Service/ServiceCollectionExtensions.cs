using System.Text;
using FastWiki.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注入FastSemanticKernel
    /// </summary>
    /// <param name="builder"></param>
    public static void AddFastSemanticKernel(this WebApplicationBuilder builder)
    {
        var handler = new OpenAIHttpClientHandler();

        builder.Services.AddScoped<Kernel>(_ =>
        {
            var kernel = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(
                    modelId: OpenAIOption.ChatModel,
                    apiKey: OpenAIOption.ChatToken,
                    httpClient: new HttpClient(handler))
                .Build();
#pragma warning disable SKEXP0050 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            kernel.ImportPluginFromObject(new ConversationSummaryPlugin(), "ConversationSummaryPlugin");
#pragma warning restore SKEXP0050 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
#pragma warning disable SKEXP0050 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            kernel.ImportPluginFromObject(new TimePlugin(), "TimePlugin");
#pragma warning restore SKEXP0050 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
            return kernel;
        });
    }


    /// <summary>
    /// 注册JWT Bearer认证服务的静态扩展方法
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services)
    {
        //使用应用密钥得到一个加密密钥字节数组
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(cfg => cfg.SlidingExpiration = true)
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }

    public static void AddLoadEnvironment(this WebApplicationBuilder builder)
    {
        var OPENAI_CHAT_ENDPOINT = Environment.GetEnvironmentVariable("OPENAI_CHAT_ENDPOINT");
        var OPENAI_CHAT_EMBEDDING_ENDPOINT = Environment.GetEnvironmentVariable("OPENAI_CHAT_EMBEDDING_ENDPOINT");
        var OPENAI_CHAT_TOKEN = Environment.GetEnvironmentVariable("OPENAI_CHAT_TOKEN");
        var OPENAI_CHAT_MODEL = Environment.GetEnvironmentVariable("OPENAI_CHAT_MODEL");
        var OPENAI_EMBEDDING_MODEL = Environment.GetEnvironmentVariable("OPENAI_EMBEDDING_MODEL");
        var OPENAI_EMBEDDING_TOKEN = Environment.GetEnvironmentVariable("OPENAI_EMBEDDING_TOKEN");

        if (!OPENAI_CHAT_ENDPOINT.IsNullOrWhiteSpace())
        {
            OpenAIOption.ChatEndpoint = OPENAI_CHAT_ENDPOINT;
        }

        if (!OPENAI_CHAT_EMBEDDING_ENDPOINT.IsNullOrWhiteSpace())
        {
            OpenAIOption.EmbeddingEndpoint = OPENAI_CHAT_EMBEDDING_ENDPOINT;
        }

        if (!OPENAI_CHAT_TOKEN.IsNullOrWhiteSpace())
        {
            OpenAIOption.ChatToken = OPENAI_CHAT_TOKEN;
        }

        if (!OPENAI_CHAT_MODEL.IsNullOrWhiteSpace())
        {
            OpenAIOption.ChatModel = OPENAI_CHAT_MODEL;
        }

        if (!OPENAI_EMBEDDING_MODEL.IsNullOrWhiteSpace())
        {
            OpenAIOption.EmbeddingModel = OPENAI_EMBEDDING_MODEL;
        }

        if (!OPENAI_EMBEDDING_TOKEN.IsNullOrWhiteSpace())
        {
            OpenAIOption.EmbeddingToken = OPENAI_EMBEDDING_TOKEN;
        }
    }
}