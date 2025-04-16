using Microsoft.EntityFrameworkCore;
using QuoteHub.Core.Database;
using QuoteHub.Core.Entities.Models;
using QuoteHub.Core.Repositories.Interfaces;

namespace QuoteHub.Core.Repositories;

public class QuoteRepository : IQuoteRepository
{
    private readonly DatabaseContext _context;
    public QuoteRepository(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<QuoteDBO?> GetQuoteByIdAsync(Guid quoteId)
    {
        return await _context.Quotes.FindAsync(quoteId);
    }
    public async Task<IEnumerable<QuoteDBO>> GetAllQuotesAsync()
    {
        return await _context.Quotes.Include(q => q.Author).Include(q => q.Language).ToListAsync();
    }
    public async Task<QuoteDBO> AddQuoteAsync(QuoteDBO quote)
    {
        await _context.Quotes.AddAsync(quote);
        await _context.SaveChangesAsync();

        return quote;
    }
    public async Task<QuoteDBO> UpdateQuoteAsync(QuoteDBO quote)
    {
        _context.Quotes.Update(quote);
        await _context.SaveChangesAsync();

        return quote;
    }
    public async Task DeleteQuoteAsync(Guid quoteId)
    {
        var quote = await GetQuoteByIdAsync(quoteId);
        if (quote != null)
        {
            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorIdAsync(Guid authorId)
    {
        return await _context.Quotes
            .Where(q => q.AuthorId == authorId)
            .ToListAsync();
    }

    public async Task<IEnumerable<QuoteDBO>> GetQuotesByLanguageIdAsync(Guid languageId)
    {
        return await _context.Quotes
            .Where(q => q.LanguageId == languageId)
            .ToListAsync();
    }
    public async Task<IEnumerable<QuoteDBO>> GetQuotesByLanguageNameAsync(string languageName)
    {
        return await _context.Quotes
            .Where(q => q.Language.Name.Contains(languageName))
            .ToListAsync();
    }
    public async Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorNameAsync(string authorName)
    {
        return await _context.Quotes
            .Where(q => q.Author.Name.Contains(authorName))
            .ToListAsync();
    }

    public async Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorAliasAsync(string authorAlias)
    {
        return await _context.Quotes
            .Where(q => q.Author.Alias.Contains(authorAlias))
            .ToListAsync();
    }

    public async Task<IEnumerable<QuoteDBO>> SearchQuoteByPartialStringAsync(string partialString)
    {
        return await _context.Quotes.Include(q => q.Author).Include(q => q.Language)
            .Where(q => q.QuoteText.Contains(partialString))
            .ToListAsync();
    }
}
