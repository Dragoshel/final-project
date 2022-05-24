using FinalProject.Data;
using FinalProject.Models;

using Dapper;
using System.Data;

namespace FinalProject.Repos;

public class TeacherDto
{
    public string Ssn { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNum { get; set; }

    public DateTime Expiration { get; set; }

    public Guid CampusID { get; set; }
}

public class StudentDto
{
    public string Ssn { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNum { get; set; }

    public DateTime Expiration { get; set; }


    public string Country { get; set; }

    public string City { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string PostCode { get; set; }
}

public class MemberRepo : IMemberRepo
{
    private readonly Engine _engine;

    public MemberRepo(Engine engine) => _engine = engine;

    private Member DapperToMember(dynamic memberObj)
    {
        var member = new Member()
        {
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

    public async Task<Member> CreateTeacherAsync(TeacherDto teacherDto)
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[CreateTeacher]";

            var memberObj = (await con.QueryAsync(SP_NAME, teacherDto, commandType: CommandType.StoredProcedure)).SingleOrDefault();

            return DapperToMember(memberObj);
        }
    }

    public async Task<Member> CreateStudentAsync(StudentDto studentDto)
    {
        using (var con = _engine.MakeConnection())
        {
            con.Open();

            const string SP_NAME = "[dbo].[CreateStudent]";

            var memberObj = (await con.QueryAsync(SP_NAME, studentDto, commandType: CommandType.StoredProcedure)).SingleOrDefault();

            return DapperToMember(memberObj);
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

            var memberObj = await con.QueryAsync(sql, new { ssn = ssn });

            return memberObj.Count() < 1 ? null : DapperToMember(memberObj.First());
        }
    }

    public async Task<int> DeleteAsync(string ssn)
    {
        var sql = @"DELETE FROM Member
                    WHERE Member.ssn=@Ssn";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            var count = await con.ExecuteAsync(sql, new { ssn = ssn });

            return count;
        }
    }

    public async Task<int> UpdateAsync(string ssn, Member member)
    {
        var sql = @"UPDATE Member
                    SET firstName=@FirstName, lastName=@LastName, phoneNum=@PhoneNum,
                        expiration=@Expiration, addressID=@AddressID, memberTypeID=@MemberTypeID
                    WHERE Member.ssn=@Ssn";

        using (var con = _engine.MakeConnection())
        {
            con.Open();

            member.Ssn = ssn;

            var count = await con.ExecuteAsync(sql, member);

            return count;
        }
    }
}