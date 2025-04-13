using QuoteHub.Core.Entities.Models;
using QuoteHub.Core.Repositories.Interfaces;
using QuoteHub.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteHub.Core.Services;

internal class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<AuthorDBO?> GetAuthorByIdAsync(Guid authorId)
    {
        return await _authorRepository.GetAuthorByIdAsync(authorId);
    }

    public async Task<IEnumerable<AuthorDBO>> GetAllAuthorsAsync()
    {
        return await _authorRepository.GetAllAuthorsAsync();
    }

    public async Task AddAuthorAsync(AuthorDBO author)
    {
        await _authorRepository.AddAuthorAsync(author);
    }

    public async Task UpdateAuthorAsync(AuthorDBO author)
    {
        await _authorRepository.UpdateAuthorAsync(author);
    }

    public async Task DeleteAuthorAsync(Guid authorId)
    {
        await _authorRepository.DeleteAuthorAsync(authorId);
    }

    public async Task<IEnumerable<AuthorDBO>> SearchAuthorByPartialNameAsync(string partialString)
    {
        return await _authorRepository.SearchAuthorByPartialNameAsync(partialString);
    }
}
