using FinalProject.Data;
using FinalProject.Models;

using Dapper;

namespace FinalProject.Controllers
{
    public class MemberController : IMemberController
    {
        private readonly Engine engine;

        public MemberController(Engine engine)
        {
            this.engine = engine;
        }

        public IEnumerable<Member> Get(int id)
        {
            using (var con = engine.connection)
            {
                return con.Query<Member>("SELECT * FROM Member WHERE Member.MemberId = @Id", new { Id = id });
            }
        }
    }
}