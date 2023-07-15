using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

//TODO - This

try
{
    Log.Information("Starting dockweb.");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddHttpClient("rest", c=>c.BaseAddress = new Uri("http://localhost:8080"));

    builder.Services.AddScoped<dockweb.IBookService,dockweb.BookService>();

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("read:books", policy =>
            policy.Requirements.Add(
                new dockweb.HasScopeRequirement("read:books", builder.Configuration["AUTHORITY"])));
        options.AddPolicy("read:authors", policy =>
            policy.Requirements.Add(
                new dockweb.HasScopeRequirement("read:authors", builder.Configuration["AUTHORITY"])));
    });

    builder.Services.AddSingleton<IAuthorizationHandler, dockweb.HasScopeHandler>();

    builder.Services
        .AddGraphQLServer()
        .AddAuthorization()
        .SetPagingOptions(new PagingOptions{
            MaxPageSize = dockweb.BookService.MaxItemsPerPage
        })
        .AddQueryType<dockweb.Query>()
        .AddTypeExtension<dockweb.BooksConnectionExtension>()
        .AddTypeExtension<dockweb.BookExtensions>()
        .AddJsonSupport()
        .AddDocumentFromString(@"
            type Publisher {
                title: String @fromJson
                name: String @fromJson
                datePublished: String @fromJson
            }
        ");

    builder.Services.AddAuthentication(options =>{
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options=>{
        options.Authority = builder.Configuration["AUTHORITY"];
        options.Audience = builder.Configuration["AUDIENCE"];
    });

    builder.Host.UseSerilog();

    var app = builder.Build();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGet("/", () => $"Say Hello {app.Environment.EnvironmentName}");

    app.MapGraphQL();

    app.RunWithGraphQLCommands(args);
}
catch (Exception ex)
{
    Log.Fatal(ex,"Terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
