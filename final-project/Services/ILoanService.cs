using FinalProject.Models;

namespace FinalProject.Services;

public interface ILoanService
{
    Task<Loan> CreateAsync(CreateLoanDto createLoanDto);

    Task<IEnumerable<IEnumerable<Guid>>> CreateMultipleAsync(IEnumerable<CreateLoanDto> createLoanDtos);

    Task<int> ReturnBook(Guid barcode);

    Task<IEnumerable<OverdueNoticeDto>> CheckOverdueLoans();

    Task<InterLibrary_Loan> LoanFromLibrary(LoanFromLibraryDto loanFromLibraryDto);
}
