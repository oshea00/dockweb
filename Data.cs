namespace dockweb;

public class DataRepo {
    readonly static List<Book> Books = new()
    {
        new Book
        {
            Title = "C# In Depth.",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        },
        new Book
        {
            Title = "C# For Dummies",
            Author = new Author
            {
                Name = "James Ritchie"
            }
        },
        new Book
        {
            Title = "Mountain Climbing For Dummies",
            Author = new Author
            {
                Name = "Jim Kirk"
            }
        },
    };

    public static IEnumerable<Book> GetBooks()
    {
        return Books;
    }

}