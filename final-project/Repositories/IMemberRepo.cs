using FinalProject.Models;

namespace FinalProject.Repos;

public interface IMemberRepo
{
    Task<Member> CreateTeacherAsync(TeacherDto teacherDto);

    Task<Member> CreateStudentAsync(StudentDto studentDto);

    Task<Member> GetAsync(string ssn);

    Task<int> DeleteAsync(string ssn);

    Task<int> UpdateAsync(string ssn, Member newMember);
}