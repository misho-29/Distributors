namespace Distributors.Application.Models.Requests;
public class AddSaleRequest
{
    public string ProductCode { get; set; } = null!;
    public int Quantity { get; set; }
    public string DistributorId { get; set; } = null!;
    public DateTime SaleDate { get; set; }
    public decimal TotalPrice { get; set; }
}
