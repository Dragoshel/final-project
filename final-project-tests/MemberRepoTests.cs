using Xunit;
using Moq;

using FinalProject.Repos;
using FinalProject.Models;

namespace FinalProjectTests;

public class MemberRepoTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public MemberRepoTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateStudent_ShouldCreateStudent_WhenStudentIsValid()
    {
        // Arrange
        using var engine = _fixture.CreateEngine();
        var _sut = new MemberRepo(engine);

        var expectedStudent = new Member()
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

        var createStudentDto = new CreateStudentDto()
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

        // Act
        var result = await _sut.CreateStudentAsync(createStudentDto);

        // Assert
        Assert.Equal(expectedStudent.Ssn, result.Ssn);
        Assert.Equal(expectedStudent.FirstName, result.FirstName);
        Assert.Equal(expectedStudent.LastName, result.LastName);
        Assert.Equal(expectedStudent.PhoneNum, result.PhoneNum);
        Assert.Equal(expectedStudent.Expiration, result.Expiration);
    }
}
