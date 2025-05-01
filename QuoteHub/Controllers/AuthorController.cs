using Microsoft.AspNetCore.Mvc;
using QuoteHub.Core.Entities.DTOs;
using QuoteHub.Core.Entities.Models;
using QuoteHub.Core.Services.Interfaces;
using QuoteHub.Enums;
using QuoteHub.Helper;

namespace QuoteHub.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    // Search author by name
    [HttpGet("search/{partialString}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AuthorDBO>>> SearchAuthor(string partialString)
    {
        BaseResponse<IEnumerable<AuthorDBO>> response = new BaseResponse<IEnumerable<AuthorDBO>>(ResponseStatus.Fail);
        IEnumerable<AuthorDBO> authors = await _authorService.SearchAuthorByPartialNameAsync(partialString);
        if (authors == null || !authors.Any())
        {
            response.Status = ResponseStatus.Fail;
            response.Message = "No authors found.";
            return NotFound(response);
        }
        response.Status = ResponseStatus.Success;
        response.Data = authors;
        return Ok(response);
    }
}
