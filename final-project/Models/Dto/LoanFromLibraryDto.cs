namespace FinalProject.Models;

public class LoanFromLibraryDto
{
    public Guid LibraryID { get; set; }

    public Guid BookCopyBarcode { get; set; }

    public DateTime DueDate { get; set; }

    public bool Direction { get; set; }
}