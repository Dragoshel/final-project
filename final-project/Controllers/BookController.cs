using Microsoft.AspNetCore.Mvc;

using FinalProject.Services;
using FinalProject.Models;

namespace FinalProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    private readonly ILogger<IBookService> _logger;

    public BookController(IBookService bookService, ILogger<IBookService> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Book>> CreateAsync(Book book)
    {
        try
        {
            await _bookService.CreateAsync(book);

            _logger.LogInformation("Created new book");

            return Ok(book);
        }
        catch (Exception)
        {
            // Ask the bois about this
            throw;
        }
    }

    [HttpGet("get/{ISBN}")]
    public async Task<ActionResult> GetAsync(string ISBN)
    {
        try
        {
            var book = await _bookService.GetAsync(ISBN);

            return Ok(book);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpDelete("delete/{isbn}")]
    public async Task<ActionResult> DeleteAsync(string ISBN)
    {
        try
        {
            await _bookService.DeleteAsync(ISBN);

            return Ok(new { Message = $"Successfully deleted book with ISBN={ISBN}" });
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut("update/{isbn}")]
    public async Task<ActionResult> UpdateAsync(string ISBN, Book book)
    {
        try
        {
            await _bookService.UpdateAsync(ISBN, book);

            return Ok(new { Message = $"Successfully updated book with ISBN={ISBN}", Book = book });
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}