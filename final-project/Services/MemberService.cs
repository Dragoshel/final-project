using FinalProject.Repos;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Services;


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

    private async Task<Member> Check_If_Member_Exists(Guid cardID)
    {
        var foundBook = await _memberRepo.GetAsync(cardID);

        return foundBook;
    } 

    public async Task<Member> CreateTeacherAsync(CreateTeacherDto createTeacherDto)
    {
        var memberSsnResult = await _memberRepo.CreateTeacherAsync(createTeacherDto);

        if (memberSsnResult is null)
            throw new FinalProjectException("Creating student member failed.");
        
        return memberSsnResult;
    }

    public async Task<Member> CreateStudentAsync(CreateStudentDto createStudentDto)
    {
        var memberSsnResult = await _memberRepo.CreateStudentAsync(createStudentDto);

        if (memberSsnResult is null)
            throw new FinalProjectException("Creating student member failed.");
        
        return memberSsnResult;
    }

    public async Task<Member> GetAsync(Guid cardID)
    {
        var member = await Check_If_Member_Exists(cardID);

        if (member is null)
            throw new FinalProjectException($"The member with card id {cardID} does not exist.");

        return member;
    }

    public async Task DeleteAsync(Guid cardID)
    {
        if (await Check_If_Member_Exists(cardID) is null)
            throw new FinalProjectException($"The member with card id {cardID} does not exist.");

        var result = await _memberRepo.DeleteAsync(cardID);

        if (result < 1)
            throw new FinalProjectException($"Could not delete member with member id {cardID}.");
    }

    public async Task UpdateAsync(Guid cardID, Member newMember)
    {
        if (await Check_If_Member_Exists(cardID) is null)
            throw new FinalProjectException($"The member with card id {cardID} does not exist.");

        var result = await _memberRepo.UpdateAsync(cardID, newMember);
        
        if (result < 1)
            throw new FinalProjectException($"Could not update member with card id {cardID}.");
    }

    public async Task<IEnumerable<Member>> GetExpiredMemberCards()
    {
        return await _memberRepo.GetExpiredMemberCards();
    }
}