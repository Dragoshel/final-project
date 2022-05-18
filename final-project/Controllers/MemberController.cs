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

    [HttpPost("create")]
    public async Task<ActionResult<Member>> CreateAsync(Member newMember)
    {
        try
        {
            await _memberService.CreateAsync(newMember);

            return Ok(newMember);
        }
        catch (Exception)
        {
            // Ask the bois about this
            throw;
        }
    }

    [HttpGet("get/{SSN}")]
    public async Task<ActionResult> GetAsync(string SSN)
    {
        try
        {
            var book = await _memberService.GetAsync(SSN);

            return Ok(book);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpDelete("delete/{SSN}")]
    public async Task<ActionResult> DeleteAsync(string SSN)
    {
        try
        {
            await _memberService.DeleteAsync(SSN);

            return Ok(new { Message = $"Successfully deleted book with SSN={SSN}" });
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut("update/{SSN}")]
    public async Task<ActionResult> UpdateAsync(string SSN, Member newMember)
    {
        try
        {
            await _memberService.UpdateAsync(SSN, newMember);

            return Ok(new { Message = $"Successfully updated book with SSN={SSN}", Book = newMember });
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}