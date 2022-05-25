using Microsoft.AspNetCore.Mvc;

using FinalProject.Services;
using FinalProject.Models;

namespace FinalProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    private readonly ILogger<IMemberService> _logger;

    public MemberController(IMemberService memberService, ILogger<IMemberService> logger)
    {
        _memberService = memberService;
        _logger = logger;
    }

    [HttpPost("create-student")]
    public async Task<ActionResult<Member>> CreateStudentAsync(CreateStudentDto createStudentDto)
    {
        try
        {
            var member = await _memberService.CreateStudentAsync(createStudentDto);

            return Ok(member);
        }
        catch (Exception)
        {
            // Ask the bois about this
            throw;
        }
    }

    [HttpPost("create-teacher")]
    public async Task<ActionResult<Member>> CreateTeacherAsync(CreateTeacherDto createTeacherDto)
    {
        try
        {
            var member = await _memberService.CreateTeacherAsync(createTeacherDto);

            return Ok(member);
        }
        catch (Exception)
        {
            // Ask the bois about this
            throw;
        }
    }

    [HttpGet("get/{cardID}")]
    public async Task<ActionResult<Member>> GetAsync(Guid cardID)
    {
        try
        {
            var member = await _memberService.GetAsync(cardID);

            return Ok(member);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpDelete("delete/{cardID}")]
    public async Task<ActionResult> DeleteAsync(Guid cardID)
    {
        try
        {
            await _memberService.DeleteAsync(cardID);

            return Ok(new { Message = $"Successfully deleted book with card id {cardID}" });
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut("update/{cardID}")]
    public async Task<ActionResult> UpdateAsync(Guid cardID, Member newMember)
    {
        try
        {
            await _memberService.UpdateAsync(cardID, newMember);

            return Ok(new { Message = $"Successfully updated member with card id {cardID}", Member = newMember });
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpGet("check-expired-cards")]
    public async Task<ActionResult<IEnumerable<Member>>> GetExpiredMemberCards()
    {
        try
        {
            var expiredMemberCards = await _memberService.GetExpiredMemberCards();

            return Ok(expiredMemberCards); 
        }
        catch (Exception)
        {
            throw;
        }
    }
}