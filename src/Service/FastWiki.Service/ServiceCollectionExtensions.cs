﻿using System.Text;
using FastWiki.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     注册JWT Bearer认证服务的静态扩展方法
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


    /// <summary>
    ///     加载环境变量
    /// </summary>
    /// <param name="builder"></param>
    public static void AddLoadEnvironment(this WebApplicationBuilder builder)
    {
        var OPENAI_CHAT_ENDPOINT = Environment.GetEnvironmentVariable("OPENAI_CHAT_ENDPOINT");
        var OPENAI_CHAT_EMBEDDING_ENDPOINT = Environment.GetEnvironmentVariable("OPENAI_CHAT_EMBEDDING_ENDPOINT");
        var OPENAI_CHAT_TOKEN = Environment.GetEnvironmentVariable("OPENAI_CHAT_TOKEN");
        var OPENAI_EMBEDDING_TOKEN = Environment.GetEnvironmentVariable("OPENAI_EMBEDDING_TOKEN");
        var DEFAULT_TYPE = Environment.GetEnvironmentVariable("DEFAULT_TYPE");
        var DEFAULT_CONNECTION = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
        var WIKI_CONNECTION = Environment.GetEnvironmentVariable("WIKI_CONNECTION");
        var QdrantPort = Environment.GetEnvironmentVariable("QDRANT_PORT");
        var QDRANT_API_KEY = Environment.GetEnvironmentVariable("QDRANT_API_KEY");
        var QDRANT_ENDPOINT = Environment.GetEnvironmentVariable("QDRANT_ENDPOINT");


        if (!OPENAI_CHAT_ENDPOINT.IsNullOrWhiteSpace()) OpenAIOption.ChatEndpoint = OPENAI_CHAT_ENDPOINT;

        if (!OPENAI_CHAT_EMBEDDING_ENDPOINT.IsNullOrWhiteSpace())
            OpenAIOption.EmbeddingEndpoint = OPENAI_CHAT_EMBEDDING_ENDPOINT;

        if (!OPENAI_CHAT_TOKEN.IsNullOrWhiteSpace()) OpenAIOption.ChatToken = OPENAI_CHAT_TOKEN;

        if (!OPENAI_EMBEDDING_TOKEN.IsNullOrWhiteSpace()) OpenAIOption.EmbeddingToken = OPENAI_EMBEDDING_TOKEN;

        if (!DEFAULT_TYPE.IsNullOrWhiteSpace()) ConnectionStringsOptions.DefaultType = DEFAULT_TYPE;

        if (!DEFAULT_CONNECTION.IsNullOrWhiteSpace()) ConnectionStringsOptions.DefaultConnection = DEFAULT_CONNECTION;

        if (!WIKI_CONNECTION.IsNullOrWhiteSpace()) ConnectionStringsOptions.WikiConnection = WIKI_CONNECTION;
        
        if (!QdrantPort.IsNullOrWhiteSpace()) ConnectionStringsOptions.QdrantPort = QdrantPort;
        
        if (!QDRANT_API_KEY.IsNullOrWhiteSpace()) ConnectionStringsOptions.QdrantAPIKey = QDRANT_API_KEY;
        
        if (!QDRANT_ENDPOINT.IsNullOrWhiteSpace()) ConnectionStringsOptions.QdrantEndpoint = QDRANT_ENDPOINT;
    }
}