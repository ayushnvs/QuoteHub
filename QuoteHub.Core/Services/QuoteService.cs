using QuoteHub.Core.Entities.DTOs;
using QuoteHub.Core.Entities.Models;
using QuoteHub.Core.Repositories.Interfaces;
using QuoteHub.Core.Services.Interfaces;

namespace QuoteHub.Core.Services;

internal class QuoteService : IQuoteService
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly IAuthorRepository _authorRepository;
    public QuoteService(IQuoteRepository quoteRepository, IAuthorRepository authorRepository)
    {
        _quoteRepository = quoteRepository;
        _authorRepository = authorRepository;
    }
    public async Task<QuoteDBO?> GetQuoteByIdAsync(Guid quoteId)
    {
        return await _quoteRepository.GetQuoteByIdAsync(quoteId);
    }
    public async Task<IEnumerable<QuoteDBO>> GetAllQuotesAsync()
    {
        return await _quoteRepository.GetAllQuotesAsync();
    }
    public async Task<QuoteDBO> AddQuoteAsync(AddQuoteRequestDTO quoteRequest)
    {
        QuoteDBO quote;

        if (quoteRequest.AuthorId == null && quoteRequest.Author != null)
        {
            AuthorDBO author = new AuthorDBO
            {
                Name = quoteRequest.Author.Name,
                Gender = quoteRequest.Author.Gender,
                Alias = quoteRequest.Author.Alias
            };

            await _authorRepository.AddAuthorAsync(author);

            quote = new QuoteDBO() { QuoteText = quoteRequest.QuoteText, AuthorId = author.Id, LanguageId = quoteRequest.LanguageId };
        }
        else
        {
            quote = new QuoteDBO() { QuoteText = quoteRequest.QuoteText, AuthorId = quoteRequest.AuthorId.Value, LanguageId = quoteRequest.LanguageId };
        }

        return await _quoteRepository.AddQuoteAsync(quote);
    }
    public async Task<QuoteDBO?> UpdateQuoteAsync(UpdateQuoteRequestDTO quoteRequest)
    {
        QuoteDBO quote;

        if (quoteRequest.AuthorId == null && quoteRequest.Author != null)
        {
            await _authorRepository.UpdateAuthorAsync(quoteRequest.Author);
            quote = new QuoteDBO
            {
                Id = quoteRequest.Id,
                QuoteText = quoteRequest.QuoteText,
                AuthorId = quoteRequest.Author.Id,
                LanguageId = quoteRequest.LanguageId
            };
        }
        else if (quoteRequest.AuthorId != null)
        {
            quote = new QuoteDBO
            {
                Id = quoteRequest.Id,
                QuoteText = quoteRequest.QuoteText,
                AuthorId = quoteRequest.AuthorId.Value,
                LanguageId = quoteRequest.LanguageId
            };
        }
        else { return null; }

        return await _quoteRepository.UpdateQuoteAsync(quote);
    }
    public async Task DeleteQuoteAsync(Guid quoteId)
    {
        await _quoteRepository.DeleteQuoteAsync(quoteId);
    }
    public async Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorIdAsync(Guid authorId)
    {
        return await _quoteRepository.GetQuotesByAuthorIdAsync(authorId);
    }
    public async Task<IEnumerable<QuoteDBO>> GetQuotesByLanguageIdAsync(Guid languageId)
    {
        return await _quoteRepository.GetQuotesByLanguageIdAsync(languageId);
    }
    public async Task<IEnumerable<QuoteDBO>> GetQuotesByLanguageNameAsync(string languageName)
    {
        return await _quoteRepository.GetQuotesByLanguageNameAsync(languageName);
    }
    public async Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorNameAsync(string authorName)
    {
        return await _quoteRepository.GetQuotesByAuthorNameAsync(authorName);
    }
    public async Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorAliasAsync(string authorAlias)
    {
        return await _quoteRepository.GetQuotesByAuthorAliasAsync(authorAlias);
    }
    public async Task<IEnumerable<QuoteDBO>> SearchQuoteByPartialStringAsync(string partialString)
    {
        return await _quoteRepository.SearchQuoteByPartialStringAsync(partialString);
    }

    //Get a random quote
    public async Task<QuoteDBO?> GetRandomQuoteAsync()
    {
        var quotes = await _quoteRepository.GetAllQuotesAsync();
        if (quotes == null || !quotes.Any())
        {
            return null;
        }
        Random random = new Random();
        int index = random.Next(quotes.Count());
        return quotes.ElementAt(index);
    }
}
