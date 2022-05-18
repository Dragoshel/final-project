using FinalProject.Models;

namespace FinalProject.Repos;

public interface IMemberRepo
{
    Task<int> CreateAsync(Member newMember);

    Task<Member> GetAsync(string ssn);

    Task<int> DeleteAsync(string ssn);

    Task<int> UpdateAsync(string ssn, Member newMember);
}