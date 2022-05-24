using FinalProject.Repos;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Services;

public class LoanService : ILoanService
{
    private readonly Engine _engine;

    private readonly ILoanRepo _loanRepo;

    private readonly ILogger<ILoanService> _logger;

    public LoanService(Engine engine, ILoanRepo loanRepo, ILogger<ILoanService> logger)
    {
        _engine = engine;
        _loanRepo = loanRepo;
        _logger = logger;
    }
    public async Task CreateAsync(int MemberCardID, int Barcode)
    {
        var result = await _loanRepo.CreateAsync(MemberCardID,Barcode);

        if (result < 1)
            throw new FinalProjectException($"Could not create loan");
    }

    public async Task ReturnBook(Guid barcode)
    {
        await _loanRepo.ReturnBook(barcode);
    }
}