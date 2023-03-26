using Distributors.Core.Enums;

namespace Distributors.Application.Models.Requests;
public class UpdateDistributorRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? ImageKey { get; set; }
    public ContactType ContactType { get; set; }
    public string ContactInfo { get; set; } = null!;
    public AddressType AddressType { get; set; }
    public string AddressInfo { get; set; } = null!;
}
