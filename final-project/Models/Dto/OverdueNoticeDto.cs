namespace FinalProject.Models;

public class OverdueNoticeDto
{
    public DateTime StartDate { get; set; }
    
    public DateTime DueDate { get; set; }

    public string Title { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string PostCode { get; set; }
}