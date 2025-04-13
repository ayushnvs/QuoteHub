using QuoteHub.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteHub.Core.Entities.DTOs;

public class AuthorDTO
{
    public required string Name { get; set; }
    public Gender Gender { get; set; }
    public string? Alias { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}
