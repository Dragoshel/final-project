using FinalProject.Models;

namespace FinalProject.Services;

public interface IMemberService
{
    Task<Member> CreateTeacherAsync(CreateTeacherDto createTeacherDto);

    Task<Member> CreateStudentAsync(CreateStudentDto createStudentDto);

    Task<Member> GetAsync(Guid cardID);

    Task<int> DeleteAsync(Guid cardID);

    Task<int> UpdateAsync(Guid cardID, Member newMember);

    Task<IEnumerable<Member>> GetExpiredMemberCards();
}