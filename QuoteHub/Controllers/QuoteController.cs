using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuoteHub.Core.Entities.DTOs;
using QuoteHub.Core.Entities.Models;
using QuoteHub.Core.Services.Interfaces;
using QuoteHub.Enums;
using QuoteHub.Helper;

namespace QuoteHub.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuoteController : ControllerBase
{
    private readonly IQuoteService _quoteService;

    public QuoteController(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuoteDBO>> AddQuoteAsync(AddQuoteRequestDTO addQuoteRequest)
    {
        BaseResponse<QuoteDBO> response = new BaseResponse<QuoteDBO>(ResponseStatus.Fail);

        if (addQuoteRequest.AuthorId == null && addQuoteRequest.Author == null) return BadRequest(response);

        QuoteDBO addedQuote = await _quoteService.AddQuoteAsync(addQuoteRequest);

        response.Data = addedQuote;
        response.Status = ResponseStatus.Success;
        response.Message = "Quote added succesfully";

        return Ok(response);
    }

    // Complete GetQuoteByIdAsync method
    [HttpGet("{quoteId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuoteDBO>> GetQuoteByIdAsync(Guid quoteId)
    {
        BaseResponse<QuoteDBO> response = new BaseResponse<QuoteDBO>(ResponseStatus.Fail);
        QuoteDBO? quote = await _quoteService.GetQuoteByIdAsync(quoteId);
        if (quote == null)
        {
            response.Message = "Quote not found";
            return NotFound(response);
        }
        response.Data = quote;
        response.Status = ResponseStatus.Success;
        response.Message = "Quote retrieved successfully";
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<QuoteDBO>>> GetAllQuotesAsync()
    {
        BaseResponse<IEnumerable<QuoteDBO>> response = new BaseResponse<IEnumerable<QuoteDBO>>(ResponseStatus.Fail);
        IEnumerable<QuoteDBO> quotes = await _quoteService.GetAllQuotesAsync();
        response.Data = quotes;
        response.Status = ResponseStatus.Success;
        response.Message = "Quotes retrieved successfully";
        return Ok(response);
    }

    [HttpPut("{quoteId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateQuoteAsync(UpdateQuoteRequestDTO quoteRequest)
    {
        BaseResponse response = new BaseResponse(ResponseStatus.Fail);

        if (quoteRequest.AuthorId == null && quoteRequest.Author == null) return BadRequest(response);

        QuoteDBO? existingQuote = await _quoteService.GetQuoteByIdAsync(quoteRequest.Id);
        if (existingQuote == null)
        {
            response.Message = "Quote not found";
            return NotFound(response);
        }
        await _quoteService.UpdateQuoteAsync(quoteRequest);
        response.Status = ResponseStatus.Success;
        response.Message = "Quote updated successfully";
        return Ok(response);
    }

    [HttpDelete("{quoteId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteQuoteAsync(Guid quoteId)
    {
        BaseResponse response = new BaseResponse(ResponseStatus.Fail);
        QuoteDBO? existingQuote = await _quoteService.GetQuoteByIdAsync(quoteId);
        if (existingQuote == null)
        {
            response.Message = "Quote not found";
            return NotFound(response);
        }
        await _quoteService.DeleteQuoteAsync(quoteId);
        response.Status = ResponseStatus.Success;
        response.Message = "Quote deleted successfully";
        return Ok(response);
    }

    [HttpGet("author/{authorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<QuoteDBO>>> GetQuotesByAuthorIdAsync(Guid authorId)
    {
        BaseResponse<IEnumerable<QuoteDBO>> response = new BaseResponse<IEnumerable<QuoteDBO>>(ResponseStatus.Fail);
        IEnumerable<QuoteDBO> quotes = await _quoteService.GetQuotesByAuthorIdAsync(authorId);
        if (quotes == null || !quotes.Any())
        {
            response.Message = "No quotes found for the specified author";
            return NotFound(response);
        }
        response.Data = quotes;
        response.Status = ResponseStatus.Success;
        response.Message = "Quotes retrieved successfully";
        return Ok(response);
    }

    [HttpGet("language/{languageId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<QuoteDBO>>> GetQuotesByLanguageIdAsync(Guid languageId)
    {
        BaseResponse<IEnumerable<QuoteDBO>> response = new BaseResponse<IEnumerable<QuoteDBO>>(ResponseStatus.Fail);
        IEnumerable<QuoteDBO> quotes = await _quoteService.GetQuotesByLanguageIdAsync(languageId);
        if (quotes == null || !quotes.Any())
        {
            response.Message = "No quotes found for the specified language";
            return NotFound(response);
        }
        response.Data = quotes;
        response.Status = ResponseStatus.Success;
        response.Message = "Quotes retrieved successfully";
        return Ok(response);
    }

    [HttpGet("language/name/{languageName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<QuoteDBO>>> GetQuotesByLanguageNameAsync(string languageName)
    {
        BaseResponse<IEnumerable<QuoteDBO>> response = new BaseResponse<IEnumerable<QuoteDBO>>(ResponseStatus.Fail);
        IEnumerable<QuoteDBO> quotes = await _quoteService.GetQuotesByLanguageNameAsync(languageName);
        if (quotes == null || !quotes.Any())
        {
            response.Message = "No quotes found for the specified language";
            return NotFound(response);
        }
        response.Data = quotes;
        response.Status = ResponseStatus.Success;
        response.Message = "Quotes retrieved successfully";
        return Ok(response);
    }

    [HttpGet("author/name/{authorName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<QuoteDBO>>> GetQuotesByAuthorNameAsync(string authorName)
    {
        BaseResponse<IEnumerable<QuoteDBO>> response = new BaseResponse<IEnumerable<QuoteDBO>>(ResponseStatus.Fail);
        IEnumerable<QuoteDBO> quotes = await _quoteService.GetQuotesByAuthorNameAsync(authorName);
        if (quotes == null || !quotes.Any())
        {
            response.Message = "No quotes found for the specified author";
            return NotFound(response);
        }
        response.Data = quotes;
        response.Status = ResponseStatus.Success;
        response.Message = "Quotes retrieved successfully";
        return Ok(response);
    }

    [HttpGet("author/alias/{authorAlias}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<QuoteDBO>>> GetQuotesByAuthorAliasAsync(string authorAlias)
    {
        BaseResponse<IEnumerable<QuoteDBO>> response = new BaseResponse<IEnumerable<QuoteDBO>>(ResponseStatus.Fail);
        IEnumerable<QuoteDBO> quotes = await _quoteService.GetQuotesByAuthorAliasAsync(authorAlias);
        if (quotes == null || !quotes.Any())
        {
            response.Message = "No quotes found for the specified author";
            return NotFound(response);
        }
        response.Data = quotes;
        response.Status = ResponseStatus.Success;
        response.Message = "Quotes retrieved successfully";
        return Ok(response);
    }

    [HttpGet("search/{partialString}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<QuoteDBO>>> SearchQuoteByPartialStringAsync(string partialString)
    {
        BaseResponse<IEnumerable<QuoteDBO>> response = new BaseResponse<IEnumerable<QuoteDBO>>(ResponseStatus.Fail);
        IEnumerable<QuoteDBO> quotes = await _quoteService.SearchQuoteByPartialStringAsync(partialString);
        if (quotes == null || !quotes.Any())
        {
            response.Message = "No quotes found matching the search criteria";
            return NotFound(response);
        }
        response.Data = quotes;
        response.Status = ResponseStatus.Success;
        response.Message = "Quotes retrieved successfully";
        return Ok(response);
    }

    // Get a random quote
    [HttpGet("random")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<QuoteDBO>> GetRandomQuoteAsync()
    {
        BaseResponse<QuoteDBO> response = new BaseResponse<QuoteDBO>(ResponseStatus.Fail);
        QuoteDBO? randomQuote = await _quoteService.GetRandomQuoteAsync();
        if (randomQuote == null)
        {
            response.Message = "No quotes found";
            return NotFound(response);
        }
        response.Data = randomQuote;
        response.Status = ResponseStatus.Success;
        response.Message = "Random quote retrieved successfully";
        return Ok(response);
    }
}