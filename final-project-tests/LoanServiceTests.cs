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
            BookCopyBarcode = barcode
        };

        _loanRepoMock.Setup(x => x.CreateAsync(It.IsAny<CreateLoanDto>()))
            .ReturnsAsync(loanMock);

        // Act
        var result = await _sut.CreateAsync(loanDTOMock);

        // Assert
        Assert.Equal(loanMock,result);
    }

    [Fact]
    public async Task CreateMultipleAsync_Should_Return_AListOfSuccessfulAndFailedLoans()
    {
        // Arrange
        Guid memberCardID1 = new Guid();
        Guid barcode1 = new Guid();
        Guid memberCardID2 = new Guid();
        Guid barcode2 = new Guid();
        Guid memberCardID3 = new Guid();
        Guid barcode3 = new Guid();
        var loanDTOMock1 = new CreateLoanDto()
        {
            MemberCardID = memberCardID1,
            Barcode = barcode1
        };
        var loanDTOMock2 = new CreateLoanDto()
        {
            MemberCardID = memberCardID2,
            Barcode = barcode2
        };
        var loanDTOMock3 = new CreateLoanDto()
        {
            MemberCardID = memberCardID3,
            Barcode = barcode3
        };
        var loanMock1 = new Loan()
        {
            ID = new Guid(),
            StartDate = new DateTime(2022, 5, 27),
            DueDate = new DateTime(2022, 9, 10),
            MemberCardID = memberCardID1,
            BookCopyBarcode = barcode1
        };
        Loan? loanMock2 = null;
        var loanMock3 = new Loan()
        {
            ID = new Guid(),
            StartDate = new DateTime(2022, 5, 22),
            DueDate = new DateTime(2022, 9, 3),
            MemberCardID = memberCardID3,
            BookCopyBarcode = barcode3
        };
        List<CreateLoanDto> input = new List<CreateLoanDto>()
        {
            loanDTOMock1,
            loanDTOMock2,
            loanDTOMock3
        };
        _loanRepoMock.Setup(x => x.CreateAsync(loanDTOMock1))
            .ReturnsAsync(loanMock1);
        _loanRepoMock.Setup(x => x.CreateAsync(loanDTOMock2))
            .ReturnsAsync(loanMock2);
        _loanRepoMock.Setup(x => x.CreateAsync(loanDTOMock3))
            .ReturnsAsync(loanMock3);
        List<List<Guid>> expected = new List<List<Guid>>()
        {
            new List<Guid>()
            {
                barcode1,
                barcode3
            },
            new List<Guid>()
            {
                barcode2
            },
        };

        // Act
        var result = await _sut.CreateMultipleAsync(input);

        // Assert
        Assert.Equal(expected, result);
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
