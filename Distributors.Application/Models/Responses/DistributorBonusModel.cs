namespace Distributors.Application.Models.Responses;
public class DistributorBonusModel
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public decimal BonusAmount { get; set; }
}