using FinalProject.Models;

namespace FinalProject.Repos;

public interface IMemberRepo
{
    Task<Member> CreateTeacherAsync(CreateTeacherDto createTeacherDto);

    Task<Member> CreateStudentAsync(CreateStudentDto createStudentDto);

    Task<Member> GetAsync(Guid cardID);

    Task<int> DeleteAsync(Guid cardID);

    Task<int> UpdateAsync(Guid cardID, Member newMember);

    Task<IEnumerable<Member>> GetExpiredMemberCards();
}