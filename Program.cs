using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try 
{
    Log.Information("Starting dockweb.");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    
    var app = builder.Build();

    app.MapGet("/", () => "Hello World!");

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

