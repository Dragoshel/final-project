using FinalProject.Data;
using FinalProject.Models;

using Dapper;
using System.Data;

namespace FinalProject.Repos;

public class MemberRepo : IMemberRepo
{
    private readonly Engine _engine;

    public MemberRepo(Engine engine) => _engine = engine;

    private Member DapperToMember(dynamic memberObj)
    {
        var member = new Member()
        {
            CardID = Guid.Parse(memberObj.cardID.ToString()),
            Ssn = memberObj.ssn,
            FirstName = memberObj.firstName,
            LastName = memberObj.lastName,
            PhoneNum = memberObj.phoneNum,
            Expiration = memberObj.expiration,
            AddressID = Guid.Parse(memberObj.addressID.ToString()),
            MemberTypeID = Guid.Parse(memberObj.memberTypeID.ToString())
        }; 

        return member;
    }

    public async Task<Member> CreateTeacherAsync(CreateTeacherDto teacherDto)
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[CreateTeacher]";

            var memberObj = (await con.QueryAsync(SP_NAME, teacherDto, commandType: CommandType.StoredProcedure)).SingleOrDefault();

            return DapperToMember(memberObj);
        }
    }

    public async Task<Member> CreateStudentAsync(CreateStudentDto studentDto)
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[CreateStudent]";

            var memberObj = (await con.QueryAsync(SP_NAME, studentDto, commandType: CommandType.StoredProcedure)).SingleOrDefault();

            return DapperToMember(memberObj);
        }
    }

    public async Task<Member> GetAsync(Guid cardID)
    {
        var sql = @"SELECT *
                    FROM Member
                    WHERE Member.cardID=@CardID";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var memberObj = await con.QueryAsync(sql, new { CardID = cardID });

            return memberObj.Count() < 1 ? null : DapperToMember(memberObj.First());
        }
    }

    public async Task<int> DeleteAsync(Guid cardID)
    {
        var sql = @"DELETE FROM Member
                    WHERE Member.cardID=@CardID";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var count = await con.ExecuteAsync(sql, new { CardID = cardID });

            return count;
        }
    }

    public async Task<int> UpdateAsync(Guid cardID, Member member)
    {
        var sql = @"UPDATE Member
                    SET firstName=@FirstName, lastName=@LastName, phoneNum=@PhoneNum,
                        expiration=@Expiration, addressID=@AddressID, memberTypeID=@MemberTypeID
                    WHERE Member.cardID=@CardID";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            member.CardID = cardID;

            var count = await con.ExecuteAsync(sql, member);

            return count;
        }
    }

    public async Task<IEnumerable<Member>> GetExpiredMemberCards()
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[CheckExpiredMemberCards]";

            var expiredMemberCards = await con.QueryAsync<Member>(SP_NAME,
                commandType: CommandType.StoredProcedure);

            return expiredMemberCards.AsEnumerable();
        }
    }
}