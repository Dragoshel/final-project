using FinalProject.Models;
using FinalProject.Repos;

namespace FinalProject.Services;

public interface IMemberService
{
    Task<Member> CreateTeacherAsync(TeacherDto teacherDto);

    Task<Member> CreateStudentAsync(StudentDto studentDto);

    Task<Member> GetAsync(Guid cardID);

    Task DeleteAsync(Guid cardID);

    Task UpdateAsync(Guid cardID, Member newMember);
}