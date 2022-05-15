namespace FinalProject.Models
{
    public class InterLibrary_Loan
    {
        public Guid ID { get; set; }

        public bool Direction { get; set; }

        public Guid BookCopyBarcode { get; set; }

        public Guid LibraryID { get; set; }
    }
}