
using HotChocolate.Authorization;

namespace dockweb;

[Authorize("read:books")]
public class Book
{
    public string Title { get; set; } = default!;
    [UsePaging]
    public IEnumerable<Author> Authors { get; set; } = default!;
}

public class Author
{
    public string Name { get; set; } = default!;
    [Authorize("read:authors")]
    public string? Address { get; set; }
}
