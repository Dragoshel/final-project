using FinalProject.Models;
using FinalProject.Repos;

namespace FinalProject.Services;

public interface IMemberService
{
    Task<Member> CreateTeacherAsync(TeacherDto teacherDto);

    Task<Member> CreateStudentAsync(StudentDto studentDto);

    Task<Member> GetAsync(string SSN);

    Task DeleteAsync(string SSN);

    Task UpdateAsync(string SSN, Member newMember);
}