using FinalProject.Models;

namespace FinalProject.Services;

public interface IMemberService
{
    Task CreateAsync(Member newMember);

    Task<Member> GetAsync(string SSN);

    Task DeleteAsync(string SSN);

    Task UpdateAsync(string SSN, Member newMember);
}