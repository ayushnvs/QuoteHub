using QuoteHub.Core.Entities.Models;

namespace QuoteHub.Core.Repositories.Interfaces;

public interface IAuthorRepository
{
    Task<AuthorDBO?> GetAuthorByIdAsync(Guid authorId);
    Task<IEnumerable<AuthorDBO>> GetAllAuthorsAsync();
    Task<AuthorDBO> AddAuthorAsync(AuthorDBO author);
    Task<AuthorDBO> UpdateAuthorAsync(AuthorDBO author);
    Task DeleteAuthorAsync(Guid authorId);
    Task<IEnumerable<AuthorDBO>> SearchAuthorByPartialNameAsync(string partialString);
}
