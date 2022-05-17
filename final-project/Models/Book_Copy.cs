namespace FinalProject.Models;

public class Book_Copy
{
    public Guid Barcode { get; set; }

    public bool IsAvailable { get; set; }

    public string BookISBN { get; set; }

    public Guid LibraryID { get; set; }
}