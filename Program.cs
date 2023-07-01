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
        .SetPagingOptions(new PagingOptions{
            MaxPageSize = dockweb.BookService.MaxItemsPerPage
        })
        .AddQueryType<dockweb.Query>()
        .AddTypeExtension<dockweb.BooksConnectionExtension>();

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
