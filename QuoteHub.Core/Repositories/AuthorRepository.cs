using Microsoft.EntityFrameworkCore;
using QuoteHub.Core.Database;
using QuoteHub.Core.Entities.Models;
using QuoteHub.Core.Repositories.Interfaces;

namespace QuoteHub.Core.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly DatabaseContext _context;

    public AuthorRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<AuthorDBO?> GetAuthorByIdAsync(Guid authorId)
    {
        return await _context.Authors.FindAsync(authorId);
    }

    public async Task<IEnumerable<AuthorDBO>> GetAllAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<AuthorDBO> AddAuthorAsync(AuthorDBO author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task<AuthorDBO> UpdateAuthorAsync(AuthorDBO author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();

        return author;
    }

    public async Task DeleteAuthorAsync(Guid authorId)
    {
        var author = await GetAuthorByIdAsync(authorId);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<QuoteDBO>> GetQuotesByAuthorIdAsync(Guid authorId)
    {
        return await _context.Quotes
            .Where(q => q.AuthorId == authorId)
            .ToListAsync();
    }

    public async Task<IEnumerable<AuthorDBO>> SearchAuthorByPartialNameAsync(string partialString)
    {
        return await _context.Authors
            .Where(a => a.Name.Contains(partialString) || a.Alias.Contains(partialString))
            .ToListAsync();
    }

}
