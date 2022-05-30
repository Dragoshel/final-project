using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Repos;

namespace FinalProjectTests;

public class LoanServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly LoanService _sut;

    private readonly Mock<ILoanRepo> _loanRepoMock = new Mock<ILoanRepo>();

    private readonly Mock<ILogger<ILoanService>> _loggerMock = new Mock<ILogger<ILoanService>>();

    private readonly DatabaseFixture _fixture;

    public LoanServiceTests(DatabaseFixture fixture)
    {
        _fixture = fixture;

        _sut = new LoanService(_fixture.engine, _loanRepoMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Return_A_Loan()
    {
        // Arrange
        Guid memberCardID = new Guid();
        Guid barcode = new Guid();
        var loanDTOMock = new CreateLoanDto()
        {
            MemberCardID = new Guid(),
            Barcode = new Guid()
        };
        var loanMock = new Loan()
        {
            ID = new Guid(),
            StartDate = new DateTime(2022,5,27),
            DueDate = new DateTime(2022,9,10),
            MemberCardID = memberCardID,
            BookCopyBarcode =barcode
        };

        _loanRepoMock.Setup(x => x.CreateAsync(It.IsAny<CreateLoanDto>()))
            .ReturnsAsync(loanMock);

        // Act
        var result = await _sut.CreateAsync(loanDTOMock);

        // Assert
        Assert.Equal(loanMock,result);
    }

    [Fact]
    public async Task ReturnBook_ShouldReturn1_WhenSuccessful()
    {
        // Arrange
        var cardID = new Guid();
        _loanRepoMock.Setup(x => x.ReturnBook(cardID))
            .ReturnsAsync(1);

        // Act
        var isSuccesful = await _sut.ReturnBook(cardID);

        // Assert
        Assert.Equal(1, isSuccesful);
    }

    [Fact]
    public async Task CheckOverdueLoans_ShouldReturnAllOverdueLoans()
    {
        // Arrange
        var overdueDTOMock1 = new OverdueNoticeDto()
        {
            StartDate = new DateTime(2022, 5, 27),
            DueDate = new DateTime(2022, 9, 10),
            Title = "Bible",
            FirstName = "Nick",
            LastName = "Smith",
            Country = "USA",
            City = "Atlanta",
            AddressLine1 = "Something street",
            AddressLine2 = "1st flooe",
            PostCode = "6666"
        };
        var overdueDTOMock2 = new OverdueNoticeDto()
        {
            StartDate = new DateTime(2022, 5, 24),
            DueDate = new DateTime(2022, 9, 7),
            Title = "Game Of Thrones",
            FirstName = "George",
            LastName = "Martin",
            Country = "USA",
            City = "Atlanta",
            AddressLine1 = "Another street",
            PostCode = "6666"
        };
        IEnumerable<OverdueNoticeDto> overdueLoans = new List<OverdueNoticeDto> { overdueDTOMock1, overdueDTOMock2 };
        _loanRepoMock.Setup(x => x.CheckOverdueLoans())
            .ReturnsAsync(overdueLoans);


        // Act
        var result = await _sut.CheckOverdueLoans();

        // Assert
        Assert.Equal(overdueLoans, result);
    }

    [Fact]
    public async Task LoanFromLibrary_ShouldReturnAnInterLibraryLoan()
    {
        // Arrange
        Guid libaryID = new Guid();
        Guid bookCopyBarcode = new Guid();
        var libraryLoanMock = new LoanFromLibraryDto()
        {
            LibraryID = libaryID,
            BookCopyBarcode = bookCopyBarcode,
            DueDate = new DateTime(2022, 12, 15),
            Direction = false
        };
        InterLibrary_Loan loan = new InterLibrary_Loan();
        _loanRepoMock.Setup(x => x.LoanFromLibrary(libraryLoanMock))
            .ReturnsAsync(loan);

        // Act
        var result = await _sut.LoanFromLibrary(libraryLoanMock);

        // Assert
        Assert.Equal(loan, result);
    }
}
