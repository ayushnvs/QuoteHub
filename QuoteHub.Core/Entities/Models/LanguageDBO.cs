using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteHub.Core.Entities.Models;

public class LanguageDBO : BaseDBO
{
    [Column("name", TypeName = "VARCHAR(100)")]
    public required string Name { get; set; }

    [Column("iso_code", TypeName = "VARCHAR(10)")]
    public required string ISOCode { get; set; }
}
