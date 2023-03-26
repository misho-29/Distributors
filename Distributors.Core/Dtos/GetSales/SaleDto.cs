namespace Distributors.Core.Dtos.GetSales;
public class SaleDto
{
    public Product Product { get; set; } = null!;
    public Distributor Distributor { get; set; } = null!;
    public DateTime SaleDate { get; set; }
    public decimal ProductCurrentPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
