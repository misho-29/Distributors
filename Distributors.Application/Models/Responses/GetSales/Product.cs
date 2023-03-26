namespace Distributors.Application.Models.Responses.GetSales;

public class Product
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
