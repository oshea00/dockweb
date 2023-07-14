namespace dockweb;

public static class DataRepo
{
    readonly static List<Book> Books = new()
    {
        new Book
        {
            Title = "C# In Depth",
            Authors = new List<Author> {
            new Author
            {
                Name = "Jon Skeet",
                Address = "Reading, UK"
            }}
        },
        new Book
        {
            Title = "C# For Dummies",
            Authors = new List<Author> {
            new Author
            {
                Name = "James Ritchie",
                Address = "Unknown"
            }}
        },
        new Book
        {
            Title = "Mountain Climbing For Dummies",
            Authors = new List<Author> {
            new Author
            {
                Name = "Jim Kirk",
                Address = "USS Enterprise"
            }}
        },
        new Book
        {
            Title = "Tell the Machine Goodnight: A Novel",
            Authors = new List<Author> {
            new Author
            {
                Name = "Katie Williams",
                Address = "Boston, MA"
            }}
        },
        new Book
        {
            Title = "What is ChatGPT Doing?",
            Authors = new List<Author> {
            new Author
            {
                Name = "Stephen Wolfram",
                Address = "London"
            }}
        },
        new Book
        {
            Title = "The Exoplanet Handbook",
            Authors = new List<Author> {
            new Author
            {
                Name = "Michael Perryman"
            }}
        },
        new Book
        {
            Title = "The Tale of the Body Thief",
            Authors = new List<Author> {
            new Author
            {
                Name = "Anne Rice"
            }}
        },
        new Book
        {
            Title = "Surely You're Joking, Mr. Feynman!",
            Authors = new List<Author> {
            new Author
            {
                Name = "Richard P. Feynman",
                Address = "Tannu Tuva"
            },
            new Author
            {
                Name = "Ralph Leighton",
                Address = "Pasadena, CA"
            },
            }
        },
        new Book
        {
            Title = "Thinking 101: How to Reason Better to Live Better",
            Authors = new List<Author> {
            new Author
            {
                Name = "Woo-kyoung Ahn"
            }}
        },
        new Book
        {
            Title = "Zen in The Art of Writing",
            Authors = new List<Author> {
            new Author
            {
                Name = "Ray Bradbury"
            }}
        },
        new Book
        {
            Title = "Dune",
            Authors = new List<Author> {
            new Author
            {
                Name = "Frank Herbert",
                Address = "Heaven"
            }}
        },
        new Book
        {
            Title = "A Universe from Nothing",
            Authors = new List<Author> {
            new Author
            {
                Name = "Lawrence Krauss",
                Address = "New York, NY"
            }}
        },
    };

    public static IAsyncEnumerable<Book> GetBooks()
    {
        return Books.OrderBy(b => b.Title).ToAsyncEnumerable();
    }

}