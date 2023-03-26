using Distributors.Core.Enums;

namespace Distributors.Core.Entities;
public class IdentityDocumentEntity : BaseEntity
{
    public IdentityDocumentType Type { get; set; }
    public string? SerialNo { get; set; }
    public string? DocumentNo { get; set; }
    public DateTime DateOfIssue { get; set; }
    public DateTime DateOfExpiry { get; set; }
    public string PersonalNo { get; set; } = null!;
    public string? IssuingAuthority { get; set; }
}
