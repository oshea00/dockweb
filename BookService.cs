using HotChocolate.Types.Pagination;

namespace dockweb
{
    public interface IBookService
    {
        Connection<Book> GetBooks(string? after, int? take);
        Book? GetBook(string title);
    }

    public class BookService : IBookService
    {
        public Connection<Book> GetBooks(string? after, int? take)
        {
            var cursor = string.IsNullOrEmpty(after) ? "" : after;
            var edges = DataRepo.GetBooks()
                .Where(book => book.Title.CompareTo(cursor) > 0)
                .Select(book => new Edge<Book>(book, book.Title))
                .OrderBy(edge => edge.Cursor)
                .Take(take ?? 1)
                .ToList();

            var hasNextPage = edges.Count > 0;
            var lastCursor = edges.Count > 0 ? edges.Last().Cursor : null;
            var pageInfo = new ConnectionPageInfo(hasNextPage, false, cursor, lastCursor);

            return new Connection<Book>(edges, pageInfo);

        }

        public Book? GetBook(string title)
        {
            return DataRepo.GetBooks().SingleOrDefault(b => b.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
        }

    }
}