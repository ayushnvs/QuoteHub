using QuoteHub.Core.Entities.Models;

namespace QuoteHub.Core.Entities.DTOs;

public class UpdateQuoteRequestDTO
{
    public required Guid Id { get; set; }
    public required string QuoteText { get; set; }
    public AuthorDBO? Author { get; set; }
    public Guid? AuthorId { get; set; }
    public Guid LanguageId { get; set; }
}
