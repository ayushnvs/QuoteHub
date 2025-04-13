using QuoteHub.Core.Entities.DTOs;
using QuoteHub.Core.Entities.Models;

namespace QuoteHub.Core.Services.Interfaces;

public interface IQuoteService
{
    Task<QuoteDBO?> GetQuoteByIdAsync(Guid quoteId);
    Task<IEnumerable<QuoteDBO>> GetAllQuotesAsync();
    Task<QuoteDBO> AddQuoteAsync(AddQuoteRequestDTO quote);
    Task UpdateQuoteAsync(QuoteDBO quote);
    Task DeleteQuoteAsync(Guid quoteId);
    Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorIdAsync(Guid authorId);
    Task<IEnumerable<QuoteDBO>> GetQuotesByLanguageIdAsync(Guid languageId);
    Task<IEnumerable<QuoteDBO>> GetQuotesByLanguageNameAsync(string languageName);
    Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorNameAsync(string authorName);
    Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorAliasAsync(string authorAlias);
    Task<IEnumerable<QuoteDBO>> SearchQuoteByPartialStringAsync(string partialString);
}
