var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

var books = new List<Book> {
    new Book{Id=1,Title="BookId1",Author="AuthorBook1" },
    new Book{Id=2,Title="BookId2",Author="AuthorBook2" },
    new Book{Id=3,Title="BookId3",Author="AuthorBook3" },
    new Book{Id=4,Title="BookId4",Author="AuthorBook4" },
};

app.MapGet("/book", () =>
{
    return books;
});
app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(x => x.Id == id);
    if (book is null) return Results.NotFound("Book not found");
    return Results.Ok(book);
});
app.MapPost("/book", (Book book) =>
{
    books.Add(book);
});
app.MapPut("/book/{id}", (Book updatedBook,int id) =>
{
    var book = books.Find(x => x.Id == id);
    if (book is null) return Results.NotFound("Book not found");
    book.Title = updatedBook.Title;
    book.Author= updatedBook.Author;
    return Results.Ok(book);
});
app.MapDelete("/book/{id}", (int id) =>
{
    var book = books.Find(x => x.Id == id);
    if (book is null) return Results.NotFound("Book not found");
    books.Remove(book);
    return Results.Ok(books);
});
app.Run();
record class Book
{
    public int Id { get; init; }
    public string Author { get; set; } = null!;
    public string Title { get; set; } = null!;
}
