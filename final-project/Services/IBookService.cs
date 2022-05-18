using FinalProject.Models;

namespace FinalProject.Services;

public interface IBookService
{
    Task CreateAsync(Book newBook);

    Task<Book> GetAsync(string ISBN);

    Task DeleteAsync(string ISBN);

    Task UpdateAsync(string ISBN, Book newBook);
}