using FinalProject.Repos;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Services;

public class BookService : IBookService
{
    private readonly Engine _engine;

    private readonly IBookRepo _bookRepo;

    private readonly ILogger<IBookService> _logger;


    public BookService(Engine engine, IBookRepo bookRepo, ILogger<IBookService> logger)
    {
        _engine = engine;
        _bookRepo = bookRepo;
        _logger = logger;
    }

    private async Task<Book> Check_If_Book_Exists(string ISBN)
    {
        var foundBook = await _bookRepo.GetAsync(ISBN);

        return foundBook;
    }

    public async Task CreateAsync(Book newBook)
    {
        var result = await _bookRepo.CreateAsync(newBook);

        if (result < 1)
            throw new FinalProjectException($"Could not create new book");
    }

    public async Task<Book> GetAsync(string ISBN)
    {
        var book = await Check_If_Book_Exists(ISBN);

        if (book is null)
            throw new FinalProjectException($"The book with ISBN={ISBN} does not exist.");

        return book;
    }

    public async Task DeleteAsync(string ISBN)
    {
        if (await Check_If_Book_Exists(ISBN) is null)
            throw new FinalProjectException($"The book with ISBN={ISBN} does not exist.");

        var result = await _bookRepo.DeleteAsync(ISBN);

        if (result < 1)
            throw new FinalProjectException($"Could not delete book with ISBN={ISBN}.");
    }

    public async Task UpdateAsync(string ISBN, Book newBook)
    {
        if (await Check_If_Book_Exists(ISBN) is null)
            throw new FinalProjectException($"The book with ISBN={ISBN} does not exist.");

        var result = await _bookRepo.UpdateAsync(ISBN, newBook);

        if (result < 1)
            throw new FinalProjectException($"Could not update book with ISBN={ISBN}.");
    }
}