using AIDotNet.Abstractions;
using AIDotNet.Claudia;
using AIDotNet.MetaGLM;
using AIDotNet.OpenAI;
using AIDotNet.Qiansail;
using AIDotNet.SparkDesk;
using AspNetCoreRateLimit;
using FastWiki.Service;
using FastWiki.Service.Backgrounds;
using FastWiki.Service.Service;
using Masa.Contrib.Authentication.Identity;
using Microsoft.AspNetCore.StaticFiles;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection(OpenAIOption.Name)
    .Get<OpenAIOption>();

builder.Configuration.GetSection(JwtOptions.Name)
    .Get<JwtOptions>();

builder.Configuration.GetSection(ConnectionStringsOptions.Name)
    .Get<ConnectionStringsOptions>();

builder
    .AddLoadEnvironment();

builder
    .AddFastSemanticKernel();

builder.Services
    .AddOpenAIService()
    .AddSparkDeskService()
    .AddMetaGLMClientV4()
    .AddClaudia()
    .AddQiansail()
    .AddKeyedSingleton<IApiChatCompletionService, OpenAiService>(OpenAIServiceOptions.ServiceName)
    .AddKeyedSingleton<IApiChatCompletionService, MetaGLMService>(MetaGLMOptions.ServiceName)
    .AddKeyedSingleton<IApiChatCompletionService, SparkDeskService>(SparkDeskOptions.ServiceName)
    .AddKeyedSingleton<IApiChatCompletionService, ClaudiaService>(ClaudiaOptions.ServiceName)
    .AddKeyedSingleton<IApiChatCompletionService, QiansailService>(QiansailOptions.ServiceName);

builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimit"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting()
    .AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
    })
    .AddAuthorization()
    .AddHostedService<QuantizeBackgroundService>()
    .AddJwtBearerAuthentication()
    .AddMemoryCache()
    .AddEndpointsApiExplorer()
    .AddMasaIdentity(options =>
    {
        options.UserId = ClaimType.DEFAULT_USER_ID;
        options.Role = "role";
    })
    .AddMapster()
    .AddHttpContextAccessor()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxxxxxxxxxxxxxx\"",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });

        options.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "FastWiki.ServiceApp",
                Version = "v1",
                Contact = new OpenApiContact { Name = "FastWiki.ServiceApp", }
            });
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml"))
            options.IncludeXmlComments(item, true);
        options.DocInclusionPredicate((docName, action) => true);
    })
    .AddMasaDbContext<WikiDbContext>(opt => { opt.UseNpgsql(); })
    .AddDomainEventBus(dispatcherOptions =>
    {
        dispatcherOptions
            .UseEventBus()
            .UseUoW<WikiDbContext>()
            .UseRepository<WikiDbContext>();
    })
    .AddResponseCompression();

builder.Services.AddAutoInject();


var app = builder.Services.AddServices(builder, option => option.MapHttpMethodsForUnmatched = ["Post"]);

app.Use((async (context, next) =>
{
    try
    {
        await next(context);

        if (context.Response.StatusCode == 404)
        {
            context.Request.Path = "/index.html";
            await next(context);
        }
    }
    catch (UserFriendlyException userFriendlyException)
    {
        context.Response.StatusCode = 400;

        await context.Response.WriteAsJsonAsync(ResultDto.CreateError(userFriendlyException.Message, "400"));
    }
    catch (Exception e)
    {
        context.Response.StatusCode = 500;

        await context.Response.WriteAsJsonAsync(ResultDto.CreateError(e.Message, "500"));
    }
}));

var fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider
{
    Mappings =
    {
        [".md"] = "application/octet-stream",
    }
};
app.UseIpRateLimiting();

app.UseResponseCompression();

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = fileExtensionContentTypeProvider
});

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/v1/chat/completions", OpenAIService.Completions)
    .WithTags("OpenAI")
    .WithGroupName("OpenAI")
    .WithDescription("OpenAI Completions")
    .WithOpenApi();

app.MapPost("/v1/feishu/completions/{id}", FeishuService.Completions)
    .WithTags("Feishu")
    .WithGroupName("Feishu")
    .WithDescription("飞书对话接入处理")
    .WithOpenApi();

app.MapGet("/api/v1/monaco", (async context =>
{
    // 获取monaco目录下的所有文件
    var files = Directory.GetFiles("monaco", "*.ts");

    var dic = new Dictionary<string, string>();

    foreach (var file in files)
    {
        var info = new FileInfo(file);
        var content = await File.ReadAllTextAsync(file);
        dic.Add(info.Name, content);
    }

    await context.Response.WriteAsJsonAsync(dic);
}));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "FastWiki.ServiceApp"));

    #region MigrationDb

    await using var context = app.Services.CreateScope().ServiceProvider.GetService<WikiDbContext>();
    {
        await context!.Database.MigrateAsync();

        // TODO: 创建vector插件如果数据库没有则需要提供支持向量的数据库。
        await context.Database.ExecuteSqlInterpolatedAsync($"CREATE EXTENSION IF NOT EXISTS vector;");
    }

    #endregion
}

await app.RunAsync();