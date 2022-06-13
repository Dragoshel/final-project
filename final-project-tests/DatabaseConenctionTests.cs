using Xunit;
using Moq;

using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Repos;

namespace FinalProjectTests;

public class DatabaseConnectionTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public DatabaseConnectionTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CheckDatabaseConnection_ShouldBeConnected_WhenConnectionIsValid()
    {
        // Arrange
        var isConnected = true;

        // Act
        var result = await _fixture.CheckConnectionAsync();

        // Assert

        Assert.Equal(isConnected, result);
    }
}