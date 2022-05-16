using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

using FinalProject.Models;
using FinalProject.Controllers;
using FinalProject.Repositories;

namespace FinalProjectTests
{
    public class BookControllerTests : IClassFixture<DatabaseFixture>
    {
        private readonly BookController sut;

        private readonly Mock<IBookRepo> bookRepoMock = new Mock<IBookRepo>();

        private readonly Mock<ILogger<IBookController>> loggerMock = new Mock<ILogger<IBookController>>();

        private readonly DatabaseFixture fixture;

        public BookControllerTests(DatabaseFixture fixture)
        {
            this.fixture = fixture;

            sut = new BookController(this.fixture.engine, this.bookRepoMock.Object, this.loggerMock.Object);
        }

        // public static IEnumerable<object[]> Books_That_Exist()
        // {
        //     yield return new object[] { "978-1119540922", new Book { ISBN = "978-1119540922" } };
        //     yield return new object[] { "123-1234567891", new Book { ISBN = "123-1234567891" } };
        // }

        [Fact]
        public async Task GetBookByISBN_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var ISBN = "978-1119540922";
            var bookMock = new Book { ISBN = ISBN };
            bookRepoMock
                .Setup(x => x.GetAsync(ISBN))
                .ReturnsAsync(bookMock);

            // Act
            var book = await sut.GetAsync(ISBN);

            // Assert
            Assert.Equal(ISBN, book.ISBN);
        }

        [Fact(Skip = "skipped")]
        public async Task GetBookByISBN_ShouldThrow_WhenBookDoesNotExist()
        {
            bookRepoMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var book = await sut.GetAsync(string.Empty);
        }

        [Fact]
        public async Task CreateBook_ShouldCreate_WhenValidAttributes()
        {
            var book = new Book {
                ISBN = "123-123456789",
                Title = "Title",
                Subject = "Subject",
                Description = "Description",
                Edition = "Edition",
                InStock = true,
                IsLendable = true
            };
            bookRepoMock.Setup(x => x.CreateAsync(It.IsAny<Book>()))
                .ReturnsAsync(1);

            await sut.CreateAsync(book);

            loggerMock.VerifyInfoWasCalled("Successfully created.");
        }
    }
}
