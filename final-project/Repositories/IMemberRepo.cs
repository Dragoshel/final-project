using FinalProject.Models;

namespace FinalProject.Repos;

public interface IMemberRepo
{
    Task<Member> CreateTeacherAsync(TeacherDto teacherDto);

    Task<Member> CreateStudentAsync(StudentDto studentDto);

    Task<Member> GetAsync(Guid cardID);

    Task<int> DeleteAsync(Guid cardID);

    Task<int> UpdateAsync(Guid cardID, Member newMember);
}