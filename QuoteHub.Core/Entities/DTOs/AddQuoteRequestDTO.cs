namespace QuoteHub.Core.Entities.DTOs;

public class AddQuoteRequestDTO
{
    public required string QuoteText { get; set; }
    public AuthorDTO? Author { get; set; }
    public Guid? AuthorId { get; set; }
    public Guid LanguageId { get; set; }
}
