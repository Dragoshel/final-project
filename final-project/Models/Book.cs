namespace FinalProject.Models
{
    public class Book
    {
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Edition { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; } 

        public bool IsLendable { get; set; }

        public bool InStock { get; set; }
    }
}
