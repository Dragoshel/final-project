namespace FinalProject.Models
{
    public class Loan
    {
        public Guid ID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public Guid MemberCardID { get; set; }

        public Guid BookCopyBarcode { get; set; }
    }
}