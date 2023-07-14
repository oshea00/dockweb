using HotChocolate.Types.Pagination;

namespace dockweb
{
    public interface IBookService
    {
        Task<Connection<Book>> GetBooks(string? after, int? take);
        Task<Book?> GetBook(string title);
    }

    public class BookService : IBookService
    {
        public static readonly int MaxItemsPerPage = 10;

        public async Task<Connection<Book>> GetBooks(string? after, int? take)
        {
            var startCursor = string.IsNullOrEmpty(after) ? "" : after;

            var edges = await DataRepo.GetBooks()
                .Where(book => book.Title.CompareTo(startCursor) > 0)
                .Select(book => new Edge<Book>(node: book, cursor: book.Title))
                .Take(take ?? MaxItemsPerPage)
                .ToListAsync();

            var lastCursor = edges.Count > 0 ? edges.Last().Cursor : null;

            var hasNextPage = false;
            if (!string.IsNullOrEmpty(lastCursor)) {
                hasNextPage = edges.Any(e=>e.Node.Title.CompareTo(lastCursor)>0);
            }

            var hasPreviousPage = false;
            if (!string.IsNullOrEmpty(startCursor)) {
                hasPreviousPage = edges.Any(e=>e.Node.Title.CompareTo(lastCursor)<0);
            }

            var pageInfo = new ConnectionPageInfo(hasNextPage, hasPreviousPage, startCursor, lastCursor);

            return new Connection<Book>(edges, pageInfo);
        }

        public async Task<Book?> GetBook(string title)
        {
            return await DataRepo.GetBooks()
                .SingleOrDefaultAsync(b => b.Title.Equals(title, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}