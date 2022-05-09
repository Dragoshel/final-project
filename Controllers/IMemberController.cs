using FinalProject.Models;

namespace FinalProject.Controllers
{
    public interface IMemberController
    {
        IEnumerable<Member> Get(int id);
    }
}