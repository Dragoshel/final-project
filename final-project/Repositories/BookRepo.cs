using FinalProject.Data;
using FinalProject.Models;

using Dapper;

namespace FinalProject.Repositories
{
    public class BookRepo : IBookRepo
    {
        private readonly Engine engine;

        public BookRepo(Engine engine)
        {
            this.engine = engine;
        }

        public async Task CreateAsync(Book newBook)
        {
            var sql = @"INSERT INTO Book
                        (ISBN, Title, Edition, Subject, Description, IsLendable, InStock, BookAuthorID)
                        VALUES (@ISBN, @Title, @Edition, @Subject, @Description, @IsLendable, @InStock, @BookAuthorID)
                        ";

            using (var con = this.engine.connection)
            {
                var count = await con.ExecuteAsync(sql, newBook);

                if (count < 1)
                    throw new DbException("Insert statement did not insert any data.");
            }
        }

        public async Task<Book> GetAsync(string ISBN)
        {
            var sql = @"SELECT *
                        FROM Book
                        WHERE Book.ISBN = @ISBN
                        ";
            using (var con = this.engine.connection)
            {
                var bookResult = await con.QueryAsync<Book>(sql, new { ISBN = ISBN });

                if (bookResult.Count() < 1)
                    throw new DbException($"Did not find book with ISBN={ISBN}.");

                return bookResult.First();
            }
        }

        public async Task DeleteAsync(string ISBN)
        {
            var sql = @"DELETE FROM Book
                        WHERE Book.ISBN=@ISBN
                        ";
            using (var con = this.engine.connection)
            {
                var count = await con.ExecuteAsync(sql);

                if (count < 1)
                    throw new DbException("Delete statement did not delete any data.");
            }
        }

        public async Task UpdateAsync(string ISBN, Book newBook)
        {
            var sql = @"UPDATE Book
                        SET Title=@Title, Edition=@Edition, Subject=@Subject, Description=@Description,
                            IsLendable=@IsLendable, InStock=@InStock, BookAuthorID=@BookAuthorID
                        WHERE Book.ISBN=@ISBN
                        ";

            using (var con = this.engine.connection)
            {
                var count = await con.ExecuteAsync(sql, newBook);

                if (count < 1)
                    throw new DbException("Update statement did not update any data.");
            }
        }
    }
}