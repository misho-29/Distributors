using Distributors.Core.DisplayTools.Filter.Models;
using Distributors.Core.DisplayTools.Pagination;
using Distributors.Core.Dtos.GetSales;
using Distributors.Core.Entities;
using X.PagedList;

namespace Distributors.Core.Repositories;
public interface ISaleRepository : IGenericRepository<SaleEntity>
{
    Task<IPagedList<SaleDto>> GetAsync(PaginationObj pagination, Filter? filter);
    Task<List<SaleEntity>> GetAsync(DateTime fromDate, DateTime toDate);
}
