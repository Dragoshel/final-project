namespace FinalProject.Models;

public class LoanFromLibraryDto
{
    public Guid LibraryID { get; set; }

    public Guid bookCopyBarcode { get; set; }

    public DateTime dueDate { get; set; }

    public bool direction { get; set; }
}