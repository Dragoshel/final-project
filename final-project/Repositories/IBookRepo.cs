using FinalProject.Models;

namespace FinalProject.Repos;

public interface IBookRepo
{
    Task<Book> CreateAsync(Book book);

    Task<Book> GetAsync(string ISBN);

    Task<int> DeleteAsync(string ISBN);

    Task<int> UpdateAsync(string ISBN, Book book);
}