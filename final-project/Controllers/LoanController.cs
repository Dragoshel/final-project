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
    public async Task<ActionResult<Loan>> CreateAsync(CreateLoanDto createLoanDto)
    {
        try
        {
            var bookResult = await _loanService.CreateAsync(createLoanDto);

            _logger.LogInformation("Created loan");

            return Ok(bookResult);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost("return-book/{bookCopyBarcode}")]
    public async Task<ActionResult> ReturnBook(Guid bookCopyBarcode)
    {
        try
        {
            await _loanService.ReturnBook(bookCopyBarcode);

            return Ok(new { Message = $"Successfully returned book with barcode {bookCopyBarcode}" });
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("check-overdue-loans")]
    public async Task<ActionResult<IEnumerable<OverdueNoticeDto>>> CheckOverdueLoans()
    {
        try
        {
            var overdueLoans = await _loanService.CheckOverdueLoans();

            return Ok(overdueLoans);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost("loan-from-library")]
    public async Task<ActionResult<InterLibrary_Loan>> LoanFromLibrary(LoanFromLibraryDto loanFromLibraryDto)
    {
        try
        { 
            var interLibrary_Loan = await _loanService.LoanFromLibrary(loanFromLibraryDto);

            return Ok(interLibrary_Loan);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
