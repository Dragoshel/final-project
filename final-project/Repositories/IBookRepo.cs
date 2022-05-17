using FinalProject.Models;

namespace FinalProject.Repos;

public interface IBookRepo
{
    Task<int> CreateAsync(Book newBook);

    Task<Book> GetAsync(string ISBN);

    Task<int> DeleteAsync(string ISBN);

    Task<int> UpdateAsync(string ISBN, Book newBook);
}