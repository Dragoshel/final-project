namespace FinalProject.Models;

public class CreateLoanDto
{
    public Guid MemberCardID { get; set; }

    public Guid Barcode { get; set; }
}