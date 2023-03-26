namespace Distributors.Core.Dtos;
public class DistributorBonusDto
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public decimal BonusAmount { get; set; }
}
