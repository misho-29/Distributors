namespace Distributors.Core.Dtos.GetSales;

public class Product
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
