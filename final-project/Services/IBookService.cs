using FinalProject.Models;

namespace FinalProject.Services;

public interface IBookService
{
    Task<Book> CreateAsync(Book newBook);

    Task<Book> GetAsync(string ISBN);

    Task<int> DeleteAsync(string ISBN);

    Task<int> UpdateAsync(string ISBN, Book newBook);
}