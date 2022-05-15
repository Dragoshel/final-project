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
    }
}