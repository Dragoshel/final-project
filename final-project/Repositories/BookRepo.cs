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

        public async Task<int> CreateAsync(Book newBook)
        {
            var sql = @"INSERT INTO Book
                        (ISBN, Title, Edition, Subject, Description, IsLendable, InStock)
                        VALUES (@ISBN, @Title, @Edition, @Subject, @Description, @IsLendable, @InStock)";

            using (var con = this.engine.connection)
            {
                var count = await con.ExecuteAsync(sql, newBook);

                return count;
            }
        }

        public async Task<Book> GetAsync(string ISBN)
        {
            var sql = @"SELECT *
                        FROM Book
                        WHERE Book.ISBN = @ISBN";

            using (var con = this.engine.connection)
            {
                var bookResult = await con.QueryAsync<Book>(sql, new { ISBN = ISBN });

                return bookResult.Count() < 1 ? null : bookResult.First();
            }
        }

        public async Task<int> DeleteAsync(string ISBN)
        {
            var sql = @"DELETE FROM Book
                        WHERE Book.ISBN=@ISBN";

            using (var con = this.engine.connection)
            {
                var count = await con.ExecuteAsync(sql);

                return count;
            }
        }

        public async Task<int> UpdateAsync(string ISBN, Book newBook)
        {
            var sql = @"UPDATE Book
                        SET Title=@Title, Edition=@Edition, Subject=@Subject, Description=@Description,
                            IsLendable=@IsLendable, InStock=@InStock
                        WHERE Book.ISBN=@ISBN";

            using (var con = this.engine.connection)
            {
                var count = await con.ExecuteAsync(sql, newBook);

                return count;
            }
        }
    }
}