using FastWiki.Service;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection(OpenAIOption.Name)
    .Get<OpenAIOption>();

builder
    .AddFastSemanticKernel();

var app = builder.Services
    .AddEndpointsApiExplorer()
    .AddMapster()
    .AddHttpContextAccessor()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "FastWiki.ServiceApp", Version = "v1",
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
    .AddAutoInject()
    .AddServices(builder, option => option.MapHttpMethodsForUnmatched = ["Post"]);

app.UseMasaExceptionHandler();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "FastWiki.ServiceApp"));

    #region MigrationDb

    using var context = app.Services.CreateScope().ServiceProvider.GetService<WikiDbContext>();
    {
        context!.Database.EnsureCreated();
    }

    #endregion
}

app.Run();