using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Repos;

namespace FinalProjectTests;

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
    public async Task CreateBook_ShouldCreateBook_WhenBookIsValid()
    {
        // Arrange
        var bookMock = new Book()
        {
            ISBN = "",
            Title = "",
            Edition = "",
            Subject = "",
            Description = "",
            IsLendable = true,
            InStock = true
        };

        _bookRepoMock.Setup(x => x.CreateAsync(It.IsAny<Book>()))
            .ReturnsAsync(bookMock);

        // Act
        var result = await _sut.CreateAsync(bookMock);

        // Assert
        Assert.Equal(result, bookMock);
    }

    [Fact]
    public async Task GetBookByISBN_ShouldReturnBook_WhenBookExists()
    {
        // Arrange
        var ISBN = "978-1119540922";
        var bookMock = new Book { ISBN = ISBN };
        _bookRepoMock.Setup(x => x.GetAsync(ISBN))
            .ReturnsAsync(bookMock);
        // Act
        var book = await _sut.GetAsync(ISBN);

        // Assert
        Assert.Equal(ISBN, book.ISBN);
    }

    [Fact]
    public async Task GetBookByISBN_ShouldThrow_WhenBookDoesNotExist()
    {
        // Arrange
        _bookRepoMock.Setup(x => x.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        // Act
        var action = async () => await _sut.GetAsync(string.Empty);

        // Assert
        var caughtException = await Assert.ThrowsAsync<FinalProjectException>(action);
        Assert.Equal("The book with ISBN  does not exist.", caughtException.Message);
    }

    [Fact]
    public async Task DeleteBookByISBN_ShouldDelete_WhenBookExists()
    {
        // Arrange
        var ISBN = "978-1119540922";
        var bookMock = new Book { ISBN = ISBN };

        _bookRepoMock.Setup(x => x.GetAsync(ISBN))
            .ReturnsAsync(bookMock);
        _bookRepoMock.Setup(x => x.DeleteAsync(ISBN))
            .ReturnsAsync(1);
        // Act
        var result = await _sut.DeleteAsync(ISBN);

        // Assert
        Assert.Equal(result, 1);
    }

    [Fact]
    public async Task UpdateBook_ShouldUpdateBook_WhenBookIsValid()
    {
        // Arrange
        var ISBN = "978-1119540922";
        var bookMock = new Book { ISBN = ISBN };

        _bookRepoMock.Setup(x => x.GetAsync(ISBN))
            .ReturnsAsync(bookMock);
        _bookRepoMock.Setup(x => x.UpdateAsync(ISBN, bookMock))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateAsync(ISBN, bookMock);

        // Assert
        Assert.Equal(result, 1);
    }
}
