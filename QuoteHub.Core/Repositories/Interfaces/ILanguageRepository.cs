using QuoteHub.Core.Entities.Models;

namespace QuoteHub.Core.Repositories.Interfaces;

public interface ILanguageRepository
{
    Task<LanguageDBO?> GetLanguageByIdAsync(Guid languageId);
    Task<IEnumerable<LanguageDBO>> GetAllLanguagesAsync();
    Task AddLanguageAsync(LanguageDBO language);
    Task UpdateLanguageAsync(LanguageDBO language);
    Task DeleteLanguageAsync(Guid languageId);
    Task<IEnumerable<LanguageDBO>> SearchLanguageByPartialNameAsync(string partialString);
}
