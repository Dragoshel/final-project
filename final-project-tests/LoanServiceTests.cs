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
    public async Task GetAsync_ShouldReturnMember_WhenMemberExists()
    {
        // Arrange
        var cardID = new Guid();
        var memberMock = new Member { CardID = cardID };
        _memberRepoMock.Setup(x => x.GetAsync(cardID))
            .ReturnsAsync(memberMock);

        // Act
        var member = await _sut.GetAsync(cardID);

        // Assert
        Assert.Equal(cardID, member.CardID);
    }

    [Fact]
    public async Task GetAsync_ShouldThrowFinalProjectException_WhenMemberDoesNotExists()
    {
        // Arrange
        var cardID = new Guid();
        _memberRepoMock.Setup(x => x.GetAsync(cardID))
            .ReturnsAsync(() => null);
        _memberRepoMock.Setup(x => x.GetAsync(cardID))
            .ReturnsAsync(() => null);

        // Act
        Func<Task<Member>> action = async () => await _sut.GetAsync(cardID);

        // Assert
        var caughtException = await Assert.ThrowsAsync<FinalProjectException>(action);
        Assert.Equal($"The member with card id {cardID} does not exist.", caughtException.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDelete_WhenMemberExists()
    {
        // Arrange
        var cardID = new Guid();
        _memberRepoMock.Setup(x => x.DeleteAsync(cardID))
            .ReturnsAsync(1);
        _memberRepoMock.Setup(x => x.GetAsync(cardID))
            .ReturnsAsync(new Member());

        // Act
        var result = await _sut.DeleteAsync(cardID);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowFinalProjectException_WhenMemberDoesNotExists()
    {
        // Arrange
        var cardID = new Guid();
        _memberRepoMock.Setup(x => x.DeleteAsync(cardID))
            .ReturnsAsync(0);
        _memberRepoMock.Setup(x => x.GetAsync(cardID))
            .ReturnsAsync(() => null);

        // Act
        Func<Task<int>> action = async () => await _sut.DeleteAsync(cardID);

        // Assert
        var caughtException = await Assert.ThrowsAsync<FinalProjectException>(action);
        Assert.Equal($"The member with card id {cardID} does not exist.", caughtException.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdate_WhenMemberIsValid()
    {
        // Arrange
        Guid cardID = new Guid();
        var memberMock = new Member()
        {
            CardID = new Guid(),
        };

        _memberRepoMock.Setup(x => x.UpdateAsync(cardID, memberMock))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.UpdateAsync(cardID, memberMock);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetExpiredMemberCards_ShouldReturnMemberCards()
    {
        // Arrange
        var memberMock1 = new Member()
        {
            CardID = new Guid(),
        };
        var memberMock2 = new Member()
        {
            CardID = new Guid(),
        };
        IEnumerable<Member> members = new List<Member> { memberMock1, memberMock2 };
        _memberRepoMock.Setup(x => x.GetExpiredMemberCards())
            .ReturnsAsync(members);

        // Act
        var result = await _sut.GetExpiredMemberCards();

        // Assert
        Assert.Equal(members, result);
    }
}
