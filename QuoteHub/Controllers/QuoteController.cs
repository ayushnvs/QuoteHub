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

}
