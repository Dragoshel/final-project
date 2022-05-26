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

    public async Task<Book> CreateAsync(Book book)
    {
        var bookResult = await _bookRepo.CreateAsync(book);

        if (bookResult is null)
            throw new FinalProjectException("Creating book failed.");
        
        return bookResult;
    }

    public async Task<Book> GetAsync(string ISBN)
    {
        var book = await _bookRepo.GetAsync(ISBN);

        if (book is null)
            throw new FinalProjectException($"The book with ISBN {ISBN} does not exist.");

        return book;
    }

    public async Task<int> DeleteAsync(string ISBN)
    {
        await GetAsync(ISBN);
        
        var result = await _bookRepo.DeleteAsync(ISBN);

        if (result < 1)
            throw new FinalProjectException($"Could not delete book with ISBN {ISBN}.");

        return result;
    }

    public async Task<int> UpdateAsync(string ISBN, Book newBook)
    {
        await GetAsync(ISBN);

        var result = await _bookRepo.UpdateAsync(ISBN, newBook);

        if (result < 1)
            throw new FinalProjectException($"Could not update book with ISBN={ISBN}.");
        
        return result;
    }
}