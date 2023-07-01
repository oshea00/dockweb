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
        readonly int _defaultItemsPerPage = 5;

        public Connection<Book> GetBooks(string? after, int? take)
        {
            var startCursor = string.IsNullOrEmpty(after) ? "" : after;
            var edges = DataRepo.GetBooks()
                .Where(book => book.Title.CompareTo(startCursor) > 0)
                .Select(book => new Edge<Book>(node: book, cursor: book.Title))
                .Take(take ?? _defaultItemsPerPage)
                .ToList();

            var lastCursor = edges.Count > 0 ? edges.Last().Cursor : null;

            var hasNextPage = false;
            if (!string.IsNullOrEmpty(lastCursor)) {
                hasNextPage = DataRepo.GetBooks()
                    .Any(b=>b.Title.CompareTo(lastCursor)>0);
            }

            var hasPreviousPage = false;
            if (!string.IsNullOrEmpty(startCursor)) {
                hasPreviousPage = DataRepo.GetBooks()
                    .Any(b=>b.Title.CompareTo(startCursor)<0);
            }

            var pageInfo = new ConnectionPageInfo(hasNextPage, hasPreviousPage, startCursor, lastCursor);

            return new Connection<Book>(edges, pageInfo);
        }

        public Book? GetBook(string title)
        {
            return DataRepo.GetBooks().SingleOrDefault(b => b.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}