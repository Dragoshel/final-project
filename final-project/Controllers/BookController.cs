using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Repositories;

using Microsoft.Extensions.Logging;

namespace FinalProject.Controllers
{
    public class BookController : IBookController
    {
        private readonly Engine engine;

        private readonly IBookRepo bookRepo;

        private readonly ILogger logger;


        public BookController(Engine engine, IBookRepo bookRepo, ILogger<IBookController> logger)
        {
            this.engine = engine;
            this.bookRepo = bookRepo;
            this.logger = logger;
        }

        private async Task<bool> Check_If_Book_Exists(string ISBN)
        {
            var foundBook = await this.bookRepo.GetAsync(ISBN);

            return foundBook is null ? false : true;
        }

        public async Task CreateAsync(Book book)
        {
            await this.bookRepo.CreateAsync(book);

            logger.LogInformation("Successfully created.");
        }

        public async Task<Book> GetAsync(string ISBN)
        {
            if (await Check_If_Book_Exists(ISBN) is false)
                throw new FinalProjectException($"The book with ISBN={ISBN} does not exist.");

            return await this.bookRepo.GetAsync(ISBN);
        }

        public async Task DeleteAsync(string ISBN)
        {
            if (await Check_If_Book_Exists(ISBN) is false)
                throw new FinalProjectException($"The book with ISBN={ISBN} does not exist.");

            var result = await this.bookRepo.DeleteAsync(ISBN);

            if (result < 1)
                throw new FinalProjectException($"Could not Delete book with ISBN={ISBN}.");
        }

        public async Task UpdateAsync(string ISBN, Book book)
        {
            if (await Check_If_Book_Exists(ISBN) is false)
                throw new FinalProjectException($"The book with ISBN={ISBN} does not exist.");

            var result = await this.bookRepo.UpdateAsync(ISBN, book);

            if (result < 1)
                throw new FinalProjectException($"Could not Update book with ISBN={ISBN}.");
        }
    }
}