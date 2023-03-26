namespace Distributors.Application.Models.Requests;
public class AddProductRequest
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
