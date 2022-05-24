using FinalProject.Models;

namespace FinalProject.Repos;

public interface ILoanRepo
{
    Task<int> CreateAsync(int MemberCardID, int Barcode);

    Task ReturnBook(Guid barcode);
}