using FinalProject.Models;

namespace FinalProject.Services;

public interface IBookService
{
    Task CreateAsync(Book book);

    Task<Book> GetAsync(string ISBN);

    Task DeleteAsync(string ISBN);

    Task UpdateAsync(string ISBN, Book book);
}