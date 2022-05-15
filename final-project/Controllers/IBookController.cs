using FinalProject.Models;

namespace FinalProject.Controllers
{
    public interface IBookController
    {
        Task CreateAsync(Book book);

        Task<Book> GetAsync(string ISBN);
    }
}