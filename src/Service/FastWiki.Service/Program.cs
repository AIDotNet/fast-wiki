using System.Text.Json;
using FastWiki.Service;
using FastWiki.Service.Backgrounds;
using FastWiki.Service.Options;
using FastWiki.Service.Service;
using Masa.Contrib.Authentication.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using Serilog.Core;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);
WebOptions.Init(builder.Configuration);
Logger logger;
if (builder.Environment.IsDevelopment())
    logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "FastWiki")
        .CreateLogger();
else
    logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "FastWiki")
        .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Configuration.GetSection(OpenAIOption.Name)
    .Get<OpenAIOption>();

builder.Configuration.GetSection(JwtOptions.Name)
    .Get<JwtOptions>();

builder.Configuration.GetSection(ConnectionStringsOptions.Name)
    .Get<ConnectionStringsOptions>();

builder
    .AddLoadEnvironment();

if (ConnectionStringsOptions.DefaultType == "sqlite")
    builder.Services.AddMasaDbContext<SqliteContext>(opt =>
    {
        opt.UseSqlite(ConnectionStringsOptions.DefaultConnection);
    });
else
    builder.Services.AddMasaDbContext<WikiDbContext>(opt =>
    {
        opt.UseNpgsql(ConnectionStringsOptions.DefaultConnection);
    });

builder.Services
    .AddMapster();

builder.Services
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
    .AddHttpContextAccessor()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxxxxxxxxxxxxxx\""
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
                Contact = new OpenApiContact { Name = "FastWiki.ServiceApp" }
            });
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml"))
            options.IncludeXmlComments(item, true);
        options.DocInclusionPredicate((docName, action) => true);
    })
    .AddMasaDbContext<WikiDbContext>(opt =>
    {
        if (ConnectionStringsOptions.DefaultType == "sqlite")
            opt.UseSqlite(ConnectionStringsOptions.DefaultConnection);
        else
            opt.UseNpgsql(ConnectionStringsOptions.DefaultConnection);
    })
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

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

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

        logger.LogError(userFriendlyException, userFriendlyException.Message);

        await context.Response.WriteAsJsonAsync(ResultDto.CreateError(userFriendlyException.Message, "400"));
    }
    catch (Exception e)
    {
        context.Response.StatusCode = 500;

        logger.LogError(e, e.Message);

        await context.Response.WriteAsJsonAsync(ResultDto.CreateError(e.Message, "500"));
    }
});

var fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider
{
    Mappings =
    {
        [".md"] = "application/octet-stream"
    }
};

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = fileExtensionContentTypeProvider
});

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();

app.Use(async (context, next) =>
{
    await next(context);

    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/index.html";
        await next(context);
    }
});

app.MapGet("/js/env.js", () =>
{
    var webEnv = new
    {
        WebOptions.DEFAULT_AVATAR,
        WebOptions.DEFAULT_USER_AVATAR,
        WebOptions.DEFAULT_INBOX_AVATAR,
        WebOptions.DEFAULT_MODEL
    };

    // 返回js
    return Results.Text($"window.thor = {JsonSerializer.Serialize(webEnv)};", "application/javascript");
});

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

app.MapGet("/api/v1/WeChatService/ReceiveMessage/{id}", WeChatService.ReceiveMessageAsync)
    .WithTags("WeChat")
    .WithGroupName("WeChat")
    .WithDescription("微信消息验证")
    .WithOpenApi();

app.MapPost("/api/v1/WeChatService/ReceiveMessage/{id}", WeChatService.ReceiveMessageAsync)
    .WithTags("WeChat")
    .WithGroupName("WeChat")
    .WithDescription("微信消息接收")
    .WithOpenApi();

app.MapGet("/api/v1/monaco", async context =>
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
});

if (app.Environment.IsDevelopment())
    app.UseSwagger()
        .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "FastWiki.ServiceApp"));


#region MigrationDb

if (ConnectionStringsOptions.DefaultType == "sqlite")
{
    await using var context = app.Services.CreateScope().ServiceProvider.GetService<SqliteContext>();
    {
        await context!.Database.MigrateAsync();
    }
}
else
{
    await using var context = app.Services.CreateScope().ServiceProvider.GetService<WikiDbContext>();
    {
        await context!.Database.MigrateAsync();

        if (!ConnectionStringsOptions.WikiType.Equals("disk", StringComparison.OrdinalIgnoreCase))
            // TODO: 创建vector插件如果数据库没有则需要提供支持向量的数据库。
            await context.Database.ExecuteSqlInterpolatedAsync($"CREATE EXTENSION IF NOT EXISTS vector;");
    }
}

#endregion

await app.RunAsync();

Log.CloseAndFlush();