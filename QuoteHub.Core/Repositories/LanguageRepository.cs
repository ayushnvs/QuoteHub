using Microsoft.EntityFrameworkCore;
using QuoteHub.Core.Database;
using QuoteHub.Core.Entities.Models;
using QuoteHub.Core.Repositories.Interfaces;

namespace QuoteHub.Core.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly DatabaseContext _context;

    public LanguageRepository(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<LanguageDBO?> GetLanguageByIdAsync(Guid languageId)
    {
        return await _context.Languages.FindAsync(languageId);

    }
    public async Task<IEnumerable<LanguageDBO>> GetAllLanguagesAsync()
    {
        return await _context.Languages.ToListAsync();
    }
    public Task AddLanguageAsync(LanguageDBO language)
    {
        throw new NotImplementedException();
    }
    public Task UpdateLanguageAsync(LanguageDBO language)
    {
        throw new NotImplementedException();
    }
    public Task DeleteLanguageAsync(Guid languageId)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<LanguageDBO>> SearchLanguageByPartialNameAsync(string partialString)
    {
        if (string.IsNullOrWhiteSpace(partialString))
        {
            return Enumerable.Empty<LanguageDBO>();
        }

        return await _context.Languages
            .Where(language => EF.Functions.Like(language.Name, $"%{partialString}%"))
            .ToListAsync();
    }
}
