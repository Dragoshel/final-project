namespace FinalProject.Models;

public class Member
{
    public Guid CardID { get; set; }

    public string Ssn { get; set; }    

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNum { get; set; }

    public DateTime Expiration { get; set; }

    public Type MemberType { get; set; } = Type.Student;

    public Guid AddressID { get; set; }

    public Guid MemberTypeID { get; set; }


    public enum Type
    {
        Teacher = 0,
        Student = 1
    }
}
