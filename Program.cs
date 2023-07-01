using HotChocolate.Types.Pagination;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting dockweb.");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddScoped<dockweb.IBookService,dockweb.BookService>();

    builder.Services
        .AddGraphQLServer()
        .SetPagingOptions(new PagingOptions {
            DefaultPageSize = 1,
        })
        .AddQueryType<dockweb.Query>();

    builder.Host.UseSerilog();
    var app = builder.Build();

    app.MapGet("/", () => $"Say Hello {app.Environment.EnvironmentName}");

    app.MapGraphQL();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex,"Terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
