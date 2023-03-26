namespace Distributors.Core.Entities;
public class SaleEntity : BaseEntity
{
    public string DistributorId { get; set; } = null!;
    public DistributorEntity? Distributor { get; set; }
    public string ProductCode { get; set; } = null!;
    public ProductEntity? Product { get; set; }
    public DateTime SaleDate { get; set; }
    public int Quantity { get; set; }
    public decimal ProductCurrentPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsBonusCalculated { get; set; } = false;
}
