using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Responses.GetSales;

namespace Distributors.Application.Services;
public interface ISaleService
{
    Task<PaginatedObject<SaleModel>> GetAsync(GetSalesRequest request);
    Task AddAsync(AddSaleRequest request);
}
