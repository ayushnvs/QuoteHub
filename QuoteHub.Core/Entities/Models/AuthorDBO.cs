using QuoteHub.Core.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteHub.Core.Entities.Models;

public class AuthorDBO : BaseDBO
{
    [Column("name", TypeName = "VARCHAR(100)")]
    public required string Name { get; set; }

    [Column("gender", TypeName = "INT")]
    public Gender Gender { get; set; }

    [Column("alias", TypeName = "VARCHAR(100)")]
    public string Alias { get; set; } = string.Empty;

    [Column("date_of_birth", TypeName = "DATE")]
    public DateOnly? DateOfBirth { get; set; } = null;

}
