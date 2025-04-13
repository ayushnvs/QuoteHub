using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteHub.Core.Entities.Models;

public class BaseDBO
{
    [Key, Column("id", TypeName = "CHAR(36)")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("is_active", TypeName = "BIT")]
    public bool IsActive { get; set; } = true;

    [Column("updated_on", TypeName = "DATETIME")]
    public DateTime UpdatedOn { get; set; }

    [Column("created_on", TypeName = "DATETIME")]
    public DateTime CreatedOn { get; set; }
}