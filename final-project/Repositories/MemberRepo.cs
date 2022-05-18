using FinalProject.Data;
using FinalProject.Models;

using Dapper;

namespace FinalProject.Repos;

public class MemberRepo : IMemberRepo
{
    private readonly Engine _engine;

    public MemberRepo(Engine engine) => _engine = engine;

    public async Task<int> CreateAsync(Member newMember)
    {
        var sql = @"INSERT INTO Member
                    (ssn, firstName, lastName, memberType, phoneNum, cardID, addressID, libraryID)
                    VALUES (@Ssn, @FirstName, @LastName, @Type, @PhoneNum, @CardID, @AddressID, @LibraryID)";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var count = await con.ExecuteAsync(sql, newMember);

            return count;
        }
    }

    public async Task<Member> GetAsync(string ssn)
    {
        var sql = @"SELECT *
                    FROM Member
                    WHERE Member.ssn=@Ssn";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var memberResult = await con.QueryAsync<Member>(sql, new { ssn = ssn });

            return memberResult.Count() < 1 ? null : memberResult.First();
        }
    }

    public async Task<int> DeleteAsync(string ssn)
    {
        var sql = @"DELETE FROM Member
                    WHERE Member.ssn=@Ssn";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var count = await con.ExecuteAsync(sql);

            return count;
        }
    }

    public async Task<int> UpdateAsync(string ssn, Member newMember)
    {
        var sql = @"UPDATE Member
                    SET firstName=@FirstName, lastName=@LastName, memberType=@Type,
                        phoneNum=@PhoneNum, cardID=@CardID, addressID=@AddressID, libraryID=@LibraryID
                    WHERE Member.ssn=@ssn";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var count = await con.ExecuteAsync(sql, newMember);

            return count;
        }
    }
}