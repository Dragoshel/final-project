using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Repos;

namespace FinalProjectTests
{
    public class BookServiceTests : IClassFixture<DatabaseFixture>
    {
        private readonly BookService _sut;

        private readonly Mock<IBookRepo> _bookRepoMock = new Mock<IBookRepo>();

        private readonly Mock<ILogger<IBookService>> _loggerMock = new Mock<ILogger<IBookService>>();

        private readonly DatabaseFixture _fixture;

        public BookServiceTests(DatabaseFixture fixture)
        {
            _fixture = fixture;

            _sut = new BookService(_fixture.engine, _bookRepoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void Test_Connection()
        {
            Console.WriteLine(_fixture.engine.connection.ConnectionString);
            Assert.Equal(_fixture.engine.connection.State.ToString(), "Closed");
        }

        [Fact]
        public async Task GetBookByISBN_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var ISBN = "978-1119540922";
            var bookMock = new Book { ISBN = ISBN };
            _bookRepoMock
                .Setup(x => x.GetAsync(ISBN))
                .ReturnsAsync(bookMock);

            // Act
            var book = await _sut.GetAsync(ISBN);

            // Assert
            Assert.Equal(ISBN, book.ISBN);
        }

        [Fact(Skip = "skipped")]
        public async Task GetBookByISBN_ShouldThrow_WhenBookDoesNotExist()
        {
            _bookRepoMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var book = await _sut.GetAsync(string.Empty);
        }

        // [Fact]
        // public async Task CreateBook_ShouldCreate_WhenValidAttributes()
        // {
        //     var book = new Book
        //     {
        //         ISBN = "123-123456789",
        //         Title = "Title",
        //         Subject = "Subject",
        //         Description = "Description",
        //         Edition = "Edition",
        //         InStock = true,
        //         IsLendable = true
        //     };
        //     _bookRepoMock.Setup(x => x.CreateAsync(It.IsAny<Book>()))
        //         .ReturnsAsync(1);

        //     await _sut.CreateAsync(book);

        //     _loggerMock.VerifyInfoWasCalled("Successfully created.");
        // }
    }
}
