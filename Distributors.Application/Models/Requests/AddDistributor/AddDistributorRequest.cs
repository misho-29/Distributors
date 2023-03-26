using Distributors.Core.Enums;

namespace Distributors.Application.Models.Requests.AddDistributor;
public class AddDistributorRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? ImageKey { get; set; }
    public IdentityDocument IdentityDocument { get; set; } = null!;
    public ContactType ContactType { get; set; }
    public string ContactInfo { get; set; } = null!;
    public AddressType AddressType { get; set; }
    public string AddressInfo { get; set; } = null!;
    public string? RecommenderId { get; set; }
}
