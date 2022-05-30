using FinalProject.Models;

namespace FinalProject.Repos;

public interface ILoanRepo
{
    Task<Loan> CreateAsync(CreateLoanDto createLoanDto);

    Task<int> ReturnBook(Guid barcode);

    Task<IEnumerable<OverdueNoticeDto>> CheckOverdueLoans();

    Task<InterLibrary_Loan> LoanFromLibrary(LoanFromLibraryDto loanFromLibraryDto);
}