namespace FinalProject.Models
{
    public class Book_Author
    {
        public Guid ID { get; set; }

        public string BookISBN { get; set; }

        public Guid AuthorID { get; set; }
    }
}