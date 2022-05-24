using Microsoft.AspNetCore.Mvc;

using FinalProject.Services;
using FinalProject.Models;


namespace FinalProject.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;

    private readonly ILogger<ILoanService> _logger;
    public LoanController(ILoanService loanService, ILogger<ILoanService> logger)
    {
        _loanService = loanService;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Book>> CreateAsync(LoanDTO loan)
    {
        try
        {
            await _loanService.CreateAsync(loan.MemberCardID,loan.Barcode);

            _logger.LogInformation("Created loan");

            return Ok();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

public class LoanDTO
{
    public int MemberCardID { get; set; }

    public int Barcode { get; set; }
}