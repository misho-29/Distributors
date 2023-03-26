using System.ComponentModel.DataAnnotations;

namespace Distributors.Core.DisplayTools.Filter.Models;

public class FilterRule
{
    [Required]
    public string Field { get; set; } = null!;

    [Required]
    public Operator Operator { get; set; }

    [Required]
    public string Value { get; set; } = null!; // Depending on Operator should be single value(1) or multiple values(1,2,3).
}
