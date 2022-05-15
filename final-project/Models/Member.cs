namespace FinalProject.Models
{
    public class Member
    {
        public string Ssn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNum { get; set; }

        public Type Type { get; set; }

        public Guid CardID { get; set; }

        public Guid AddressID { get; set; }

        public Guid LibraryID { get; set; }

        public Guid TypeID { get; set; }
    }

    public enum Type
    {
        Teacher = 0,
        Student = 1
    }
}
