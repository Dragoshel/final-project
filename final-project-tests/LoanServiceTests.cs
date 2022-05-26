using Microsoft.Extensions.Logging;
using Xunit;
using Moq;

using FinalProject.Services;
using FinalProject.Repos;

namespace FinalProjectTests;

public class LoanServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly LoanService _sut;

    private readonly Mock<ILoanRepo> _loanRepo = new Mock<ILoanRepo>();

    private readonly Mock<ILogger<ILoanService>> _loggerMock = new Mock<ILogger<ILoanService>>();

    private readonly DatabaseFixture _fixture;

    public LoanServiceTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _sut = new LoanService(_fixture.engine, _loanRepo.Object, _loggerMock.Object);
    }

    // [Fact]
    // public async Task 
}