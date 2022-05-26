using FinalProject.Data;
using FinalProject.Models;

using Dapper;

namespace FinalProject.Repos;

public class BookRepo : IBookRepo
{
    private readonly Engine _engine;

    public BookRepo(Engine engine) => _engine = engine;

    public async Task<Book> CreateAsync(Book book)
    {
        var sql = @"INSERT INTO Book
                    (isbn, title, edition, subject, description, isLendable, inStock)
                    OUTPUT INSERTED.*
                    VALUES (@ISBN, @Title, @Edition, @Subject, @Description, @IsLendable, @InStock)";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var bookResult = await con.QueryAsync<Book>(sql, book);

            return bookResult.SingleOrDefault();
        }
    }

    public async Task<Book> GetAsync(string ISBN)
    {
        var sql = @"SELECT *
                    FROM Book
                    WHERE Book.isbn=@ISBN";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var bookResult = await con.QueryAsync<Book>(sql, new { ISBN = ISBN });

            return bookResult.SingleOrDefault();
        }
    }

    public async Task<int> DeleteAsync(string ISBN)
    {
        var sql = @"DELETE FROM Book
                    WHERE Book.isbn=@ISBN";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var count = await con.ExecuteAsync(sql);

            return count;
        }
    }

    public async Task<int> UpdateAsync(string ISBN, Book newBook)
    {
        var sql = @"UPDATE Book
                    SET title=@Title, edition=@Edition, subject=@Subject,
                    description=@Description, isLendable=@IsLendable, inStock=@InStock
                    WHERE Book.isbn=@ISBN";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var count = await con.ExecuteAsync(sql, newBook);

            return count;
        }
    }
}