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

    public async Task<Member> CreateTeacherAsync(CreateTeacherDto createTeacherDto)
    {
        var memberSsnResult = await _memberRepo.CreateTeacherAsync(createTeacherDto);

        if (memberSsnResult is null)
            throw new FinalProjectException("Creating teacher member failed.");
        
        return memberSsnResult;
    }

    public async Task<Member> CreateStudentAsync(CreateStudentDto createStudentDto)
    {
        var memberResult = await _memberRepo.CreateStudentAsync(createStudentDto);

        if (memberResult is null)
            throw new FinalProjectException("Creating student member failed.");
        
        return memberResult;
    }

    public async Task<Member> GetAsync(Guid cardID)
    {
        var memberResult = await _memberRepo.GetAsync(cardID);

        if (memberResult is null)
            throw new FinalProjectException($"The member with card id {cardID} does not exist.");
        
        return memberResult;
    }

    public async Task<int> DeleteAsync(Guid cardID)
    {
        await GetAsync(cardID);

        return await _memberRepo.DeleteAsync(cardID);
    }

    public async Task UpdateAsync(Guid cardID, Member newMember)
    {
        await _memberRepo.UpdateAsync(cardID, newMember);
    }

    public async Task<IEnumerable<Member>> GetExpiredMemberCards()
    {
        return await _memberRepo.GetExpiredMemberCards();
    }
}