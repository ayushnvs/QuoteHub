using QuoteHub.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteHub.Core.Services.Interfaces;

public interface IAuthorService
{
    Task<AuthorDBO?> GetAuthorByIdAsync(Guid authorId);
    Task<IEnumerable<AuthorDBO>> GetAllAuthorsAsync();
    Task AddAuthorAsync(AuthorDBO author);
    Task UpdateAuthorAsync(AuthorDBO author);
    Task DeleteAuthorAsync(Guid authorId);
    Task<IEnumerable<AuthorDBO>> SearchAuthorByPartialNameAsync(string partialString);

}
