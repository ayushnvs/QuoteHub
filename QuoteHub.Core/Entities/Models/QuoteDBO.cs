using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteHub.Core.Entities.Models;

public class QuoteDBO : BaseDBO
{
    [Column("quote_text", TypeName = "VARCHAR(5000)")]
    public required string QuoteText { get; set; }

    [Column("fk_author_id", TypeName = "CHAR(36)")]
    public required Guid AuthorId { get; set; }

    [Column("fk_language_id", TypeName = "CHAR(36)")]
    public required Guid LanguageId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public AuthorDBO? Author { get; set; }

    [ForeignKey(nameof(LanguageId))]
    public LanguageDBO? Language { get; set; }
}
