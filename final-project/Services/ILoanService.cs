using FinalProject.Models;

namespace FinalProject.Services;

public interface ILoanService
{
    Task CreateAsync(int MemberCardID, int Barcode);

    Task ReturnBook(Guid barcode);

    Task<IEnumerable<OverdueNoticeDto>> CheckOverdueLoans();

    Task<InterLibrary_Loan> LoanFromLibrary(LoanFromLibraryDto loanFromLibraryDto);
}
