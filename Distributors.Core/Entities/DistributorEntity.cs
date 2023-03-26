using Distributors.Core.Enums;

namespace Distributors.Core.Entities;
public class DistributorEntity : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? ImageKey { get; set; }
    public IdentityDocumentEntity? IdentityDocument { get; set; }
    public string IdentityDocumentId { get; set; } = null!;
    public ContactType ContactType { get; set; }
    public string ContactInfo { get; set; } = null!;
    public AddressType AddressType { get; set; }
    public string AddressInfo { get; set; } = null!;
    public DistributorEntity? Recommender { get; set; }
    public string? RecommenderId { get; set; }
    public decimal BonusAmount { get; set; }
}
