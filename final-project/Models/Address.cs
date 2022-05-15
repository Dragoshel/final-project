namespace FinalProject.Models
{
    public class Address
    {
        public Guid ID { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostCode { get; set; }
    }
}