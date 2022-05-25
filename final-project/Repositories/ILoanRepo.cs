using FinalProject.Models;

namespace FinalProject.Repos;

public interface ILoanRepo
{
    Task<int> CreateAsync(int MemberCardID, int Barcode);

    Task ReturnBook(Guid barcode);

    Task<IEnumerable<OverdueNoticeDto>> CheckOverdueLoans();

    Task<InterLibrary_Loan> LoanFromLibrary(LoanFromLibraryDto loanFromLibraryDto);
}