namespace FinalProject.Models
{
    public class Author
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid BookAuthorID { get; set; }
    }
}