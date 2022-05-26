using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Repos;

namespace FinalProjectTests;

public class MemberServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly MemberService _sut;

    private readonly Mock<IMemberRepo> _memberRepoMock = new Mock<IMemberRepo>();

    private readonly Mock<ILogger<IMemberService>> _loggerMock = new Mock<ILogger<IMemberService>>();

    private readonly DatabaseFixture _fixture;

    public MemberServiceTests(DatabaseFixture fixture)
    {
        _fixture = fixture;

        _sut = new MemberService(_fixture.engine, _memberRepoMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Create_Student_Should_Return_A_Student()
    {
        // Arrange
        var memberDTOMock = new CreateStudentDto()
        {
            Ssn = "123456789",
            FirstName = "Yusuke",
            LastName = "Kitagawa",
            PhoneNum = "+12 (34) 567-89",
            Expiration = new DateTime(2023, 12, 24),
            Country = "USA",
            City = "Atlanta",
            AddressLine1 = "Maple Street 8",
            AddressLine2 = null,
            PostCode = "30303"
        };
        var memberMock = new Member()
        {
            CardID = new Guid(),
            Ssn = "123456789",
            FirstName = "Yusuke",
            LastName = "Kitagawa",
            PhoneNum = "+12 (34) 567-89",
            Expiration = new DateTime(2023, 12, 24),
            MemberType = Member.Type.Student,
            AddressID = new Guid(),
            MemberTypeID = new Guid()
        };

        _memberRepoMock.Setup(x => x.CreateStudentAsync(It.IsAny<CreateStudentDto>()))
            .ReturnsAsync(memberMock);

        // Act
        var result = await _sut.CreateStudentAsync(memberDTOMock);

        // Assert
        Assert.Equal(result, memberMock);
    }

    [Fact]
    public async Task Create_Teacher_Should_Return_A_Teacher()
    {
    // Arrange
    var memberDTOMock = new CreateTeacherDto()
    {
        Ssn = "123456789",
        FirstName = "Sadayo",
        LastName = "Kawakami",
        PhoneNum = "+12 (34) 567-89",
        Expiration = new DateTime(2023, 12, 24),
        CampusID = new Guid()
    };
    var memberMock = new Member()
    {
        CardID = new Guid(),
        Ssn = "123456789",
        FirstName = "Sadayo",
        LastName = "Kawakami",
        PhoneNum = "+12 (34) 567-89",
        Expiration = new DateTime(2023, 12, 24),
        MemberType = Member.Type.Teacher,
        AddressID = new Guid(),
        MemberTypeID = new Guid()
    };

    _memberRepoMock.Setup(x => x.CreateTeacherAsync(It.IsAny<CreateTeacherDto>()))
        .ReturnsAsync(memberMock);

    // Act
    var result = await _sut.CreateTeacherAsync(memberDTOMock);

    // Assert
    Assert.Equal(result, memberMock);
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

        // Act
        Func<Task<Member>> action = async () => await _sut.GetAsync(cardID);

        // Assert
        var caughtException = await Assert.ThrowsAsync<FinalProjectException>(action);
        Assert.Equal("The member with card id " + cardID + " does not exist.", caughtException.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDelete_WhenMemberExists()
    {
        // Arrange
        var cardID = new Guid();
        _memberRepoMock.Setup(x => x.DeleteAsync(cardID))
            .ReturnsAsync(1);

        // Act
        var result = await _sut.DeleteAsync(cardID);

        // Assert
        Assert.Equal(result, 1);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowFinalProjectException_WhenMemberDoesNotExists()
    {
        // Arrange
        var cardID = new Guid();
        _memberRepoMock.Setup(x => x.DeleteAsync(cardID))
            .ReturnsAsync(0);

        // Act
        Func<Task<int>> action = async () => await _sut.DeleteAsync(cardID);

        // Assert
        var caughtException = await Assert.ThrowsAsync<FinalProjectException>(action);
        Assert.Equal("The member with card id " + cardID + " does not exist.", caughtException.Message);
    }
}
