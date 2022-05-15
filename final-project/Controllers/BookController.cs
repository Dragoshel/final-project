using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Repositories;

using Dapper;

namespace FinalProject.Controllers
{
    public class BookController : IBookController
    {
        private readonly Engine engine;

        private readonly IBookRepo bookRepo;

        public BookController(Engine engine, IBookRepo bookRepo)
        {
            this.engine = engine;
            this.bookRepo = bookRepo;
        }

        public async Task CreateAsync(Book book)
        {
            // var newBook = new Book()
            // {
            //     ISBN = "978-1119540922",
            //     Title = "Hunting Cyber Criminals: A Hacker's Guide to Online Intelligence Gathering Tools and Techniques",
            //     Edition = "1st Edition",
            //     Subject = "Cybercrimology",
            //     Description = "The skills and tools for collecting, verifying and correlating information from different types of systems is an essential skill when tracking down hackers.",
            //     IsLendable = true,
            //     InStock = true,
            // };

            await this.bookRepo.CreateAsync(book);
        }

        public async Task<Book> GetAsync(string ISBN)
        {
            return await this.bookRepo.GetAsync(ISBN);
        }

        public async Task DeleteAsync(string ISBN)
        {
            await this.bookRepo.DeleteAsync(ISBN);
        }

        public async Task UpdateAsync(string ISBN, Book book)
        {
            await this.bookRepo.UpdateAsync(ISBN, book);
        }
    }
}