namespace QuoteHub.Core;
using Microsoft.Extensions.DependencyInjection;
using QuoteHub.Core.Repositories;
using QuoteHub.Core.Repositories.Interfaces;
using QuoteHub.Core.Services;
using QuoteHub.Core.Services.Interfaces;

public static class ServiceCollectionExtension
{
    public static void AddServiceCollection(this IServiceCollection services)
    {
        // Add your services here
        services.AddScoped<IQuoteService, QuoteService>();
        services.AddScoped<IAuthorService, AuthorService>();

        // Add repositories here
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
    }
}
