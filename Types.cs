namespace dockweb;

public class Book
{
    public string Title { get; set; } = default!;

    public Author Author { get; set; } = default!;
}

public class Author
{
    public string Name { get; set; } = default!;
}
