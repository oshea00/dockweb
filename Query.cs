using HotChocolate.Types.Pagination;

namespace dockweb;

public class Query
{
    [UsePaging]
    public Connection<Book> GetBooks([Service] IBookService bookService, string? after, int? first) =>
        bookService.GetBooks(after,first);

    public Book? GetBook([Service] IBookService bookService, string title) =>
        bookService.GetBook(title);
}
