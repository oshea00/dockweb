
using System.Text.Json;
using HotChocolate.Authorization;
using Serilog;

namespace dockweb;

[Authorize("read:books")]
public class Book
{
    public string Title { get; set; } = default!;
    [UsePaging]
    public IEnumerable<Author> Authors { get; set; } = default!;
}

[ExtendObjectType<Book>]
public class BookExtensions
{
    [GraphQLType("Publisher")]
    public async Task<JsonElement> GetPublisherAsync(
        [Parent] Book book,
        [Service] IHttpClientFactory clientFactory,
        CancellationToken cancellationToken)
    {
        try
        {
            using var client = clientFactory.CreateClient("rest");
            var content = await client.GetByteArrayAsync($"book/{Uri.EscapeDataString(book.Title)}", cancellationToken);
            return JsonDocument.Parse(content).RootElement;
        } 
        catch (Exception ex)
        {
            Log.Warning($"Error fetching publisher for {book.Title}: {ex.Message}");
            return JsonDocument.Parse("{}").RootElement;
        }
    }
}

public class Author
{
    public string Name { get; set; } = default!;
    [Authorize("read:authors")]
    public string? Address { get; set; }
}
