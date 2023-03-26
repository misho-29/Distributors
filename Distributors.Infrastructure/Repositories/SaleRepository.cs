using Distributors.Core.DisplayTools.Filter;
using Distributors.Core.DisplayTools.Filter.Models;
using Distributors.Core.DisplayTools.Pagination;
using Distributors.Core.Dtos.GetSales;
using Distributors.Core.Entities;
using Distributors.Core.Repositories;
using Distributors.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Distributors.Infrastructure.Repositories;
public class SaleRepository : GenericRepository<SaleEntity>, ISaleRepository
{
    public SaleRepository(DistributorsDbContext context) : base(context)
    {
    }

    public async Task<List<SaleEntity>> GetAsync(DateTime fromDate, DateTime toDate)
    {
        return await Set.Where(sale => sale.SaleDate.Date >= fromDate.Date && sale.SaleDate.Date <= toDate.Date && !sale.IsBonusCalculated).ToListAsync();
    }

    public async Task<IPagedList<SaleDto>> GetAsync(PaginationObj pagination, Filter? filter)
    {
        return await Set.Include(sale => sale.Distributor).Include(sale => sale.Product)
            .Select(sale => new SaleDto
            {
                Distributor = new Distributor
                {
                    Id = sale.Distributor!.Id,
                    FirstName = sale.Distributor.FirstName,
                    LastName = sale.Distributor.LastName,
                },
                Product = new Product
                {
                    Code = sale.Product!.Code,
                    Name = sale.Product.Name,
                    Price = sale.Product.Price
                },
                ProductCurrentPrice = sale.ProductCurrentPrice,
                Quantity = sale.Quantity,
                SaleDate = sale.SaleDate,
                TotalPrice = sale.TotalPrice,
            }).BuildFilterQuery(filter).ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
    }
}
