using FinalProject.Models;

namespace FinalProject.Repositories
{
    public interface IBookRepo
    {
        Task CreateAsync(Book newBook);

        Task<Book> GetAsync(string ISBN);

        Task DeleteAsync(string ISBN);

        Task UpdateAsync(string ISBN, Book newBook);
    }
}