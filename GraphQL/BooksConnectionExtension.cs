using HotChocolate.Types.Pagination;

namespace dockweb;

[ExtendObjectType("BooksConnection")]
public class BooksConnectionExtension
{
    public int PageLength([Parent] Connection<Book> connection)
    {
        return connection.Edges.Count;
    }
}