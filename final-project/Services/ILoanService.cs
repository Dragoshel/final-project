namespace FinalProject.Services;

public interface ILoanService
{
    Task CreateAsync(int MemberCardID, int Barcode);

    Task ReturnBook(Guid barcode);
}
