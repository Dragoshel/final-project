using FinalProject.Models;
using FinalProject.Repos;

namespace FinalProject.Services;

public interface IMemberService
{
    Task<Member> CreateTeacherAsync(CreateTeacherDto createTeacherDto);

    Task<Member> CreateStudentAsync(CreateStudentDto createStudentDto);

    Task<Member> GetAsync(Guid cardID);

    Task DeleteAsync(Guid cardID);

    Task UpdateAsync(Guid cardID, Member newMember);
}