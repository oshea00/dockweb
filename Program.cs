using HotChocolate.Types.Pagination;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting dockweb.");

    var builder = WebApplication.CreateBuilder(args);

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
        .AddTypeExtension<dockweb.BooksConnectionExtension>();

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
