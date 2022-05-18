using FinalProject.Repos;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Services;

public class MemberDto
{
    public string Ssn { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNum { get; set; }

    public Member.Type MemberType { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string PostCode { get; set; }
}

public class MemberService : IMemberService
{
    private readonly Engine _engine;

    private readonly IMemberRepo _memberRepo;

    private readonly ILogger<IMemberService> _logger;


    public MemberService(Engine engine, IMemberRepo memberRepo, ILogger<IMemberService> logger)
    {
        _engine = engine;
        _memberRepo = memberRepo;
        _logger = logger;
    }

    private async Task<Member> Check_If_Member_Exists(string ISBN)
    {
        var foundBook = await _memberRepo.GetAsync(ISBN);

        return foundBook;
    }

    public async Task CreateAsync(Member newMember)
    {
        // var address = new Address() {
        //     Country = newMember.Country,
        //     City = newMember.City,
        //     AddressLine1 = newMember.AddressLine1,
        //     AddressLine2 = newMember.AddressLine2,
        //     PostCode = newMember.PostCode
        // };

        var result = await _memberRepo.CreateAsync(newMember);

        if (result < 1)
            throw new FinalProjectException($"Could not create new member");
    }

    public async Task<Member> GetAsync(string SSN)
    {
        var member = await Check_If_Member_Exists(SSN);

        if (member is null)
            throw new FinalProjectException($"The member with SSN={SSN} does not exist.");

        return member;
    }

    public async Task DeleteAsync(string SSN)
    {
        if (await Check_If_Member_Exists(SSN) is null)
            throw new FinalProjectException($"The member with SSN={SSN} does not exist.");

        var result = await _memberRepo.DeleteAsync(SSN);

        if (result < 1)
            throw new FinalProjectException($"Could not delete member with SSN={SSN}.");
    }

    public async Task UpdateAsync(string SSN, Member newMember)
    {
        if (await Check_If_Member_Exists(SSN) is null)
            throw new FinalProjectException($"The member with SSN={SSN} does not exist.");

        var result = await _memberRepo.UpdateAsync(SSN, newMember);

        if (result < 1)
            throw new FinalProjectException($"Could not update member with SSN={SSN}.");
    }
}