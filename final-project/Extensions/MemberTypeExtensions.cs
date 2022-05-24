using FinalProject.Models;

namespace FinalProject.Extensions;

public static class MemberTypeExtensions
{
    public static string Name(this Member.Type @this)
    {
        return Enum.GetName(typeof(Member.Type), @this);
    }
}