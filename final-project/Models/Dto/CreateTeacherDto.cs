namespace FinalProject.Models;

public class CreateTeacherDto
{
    public string Ssn { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNum { get; set; }

    public DateTime Expiration { get; set; }

    public Guid CampusID { get; set; }
}