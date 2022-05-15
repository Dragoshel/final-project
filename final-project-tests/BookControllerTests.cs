using Xunit;
using Moq;
using System.Threading.Tasks;

using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Controllers;
using FinalProject.Repositories;

namespace FinalProjectTests
{
    public class BookControllerTests
    {
        private readonly BookController sut;

        private readonly Mock<IBookRepo> bookRepoMock = new Mock<IBookRepo>();

        private readonly Mock<Engine> engineMock = new Mock<Engine>();

        public BookControllerTests()
        {
            sut = new BookController(engineMock.Object, bookRepoMock.Object);
        }

        [Fact]
        public async Task GetBookByISBN_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var ISBN = "978-1119540922";
            var bookMock = new Book { ISBN = ISBN };
            bookRepoMock.Setup(x => x.GetAsync(ISBN)).ReturnsAsync(bookMock);

            // Act
            var book = await sut.GetAsync(ISBN);

            // Assert
            Assert.Equal(ISBN, book.ISBN);
        }
    }
}
