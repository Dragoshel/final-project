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
    public async Task<Loan> CreateAsync(CreateLoanDto createLoanDto)
    {
        var loanResult = await _loanRepo.CreateAsync(createLoanDto);

        if (loanResult is null)
            throw new FinalProjectException($"Could not create loan.");
        
        return loanResult;
    }

    public async Task<int> ReturnBook(Guid barcode)
    {
        return await _loanRepo.ReturnBook(barcode);
    }

    public async Task<IEnumerable<OverdueNoticeDto>> CheckOverdueLoans()
    {
        return await _loanRepo.CheckOverdueLoans();
    }

    public async Task<InterLibrary_Loan> LoanFromLibrary(LoanFromLibraryDto loanFromLibraryDto)
    {
        return await _loanRepo.LoanFromLibrary(loanFromLibraryDto);
    }
}