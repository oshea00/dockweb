using HotChocolate.Authorization;
using HotChocolate.Types.Pagination;

namespace dockweb;

public class Query
{
    [UsePaging]
    public async Task<Connection<Book>> GetBooks([Service] IBookService bookService, string? after, int? first) => 
        await bookService.GetBooks(after,first);

    public async Task<Book?> GetBook([Service] IBookService bookService, string title) =>
        await bookService.GetBook(title);
}
