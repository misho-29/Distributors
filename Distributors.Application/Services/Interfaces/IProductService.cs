using Distributors.Application.Models.Requests;

namespace Distributors.Application.Services;
public interface IProductService
{
    Task AddAsync(AddProductRequest request);
}
