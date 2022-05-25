namespace FinalProject.Models;

public class CreateStudentDto
{
    public string Ssn { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNum { get; set; }

    public DateTime Expiration { get; set; }


    public string Country { get; set; }

    public string City { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string PostCode { get; set; }
}