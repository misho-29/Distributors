using Distributors.Core.DisplayTools.Filter;
using Distributors.Core.DisplayTools.Filter.Models;
using Distributors.Core.DisplayTools.Pagination;
using Distributors.Core.Dtos;
using Distributors.Core.Entities;
using Distributors.Core.Repositories;
using Distributors.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Distributors.Infrastructure.Repositories;
public class DistributorRepository : GenericRepository<DistributorEntity>, IDistributorRepository
{
    public DistributorRepository(DistributorsDbContext context) : base(context)
    {
    }

    public async Task<IPagedList<DistributorEntity>> GetAsync(PaginationObj pagination)
    {
        return await Set.ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
    }

    public async Task<bool> IsDistributorRecommender(string distributorId)
    {
        return await Set.AnyAsync(distributor => distributor.RecommenderId == distributorId);
    }

    public async Task<IPagedList<DistributorBonusDto>> GetAsync(PaginationObj pagination, Filter? filter)
    {
        return await Set.Select(distributor => new DistributorBonusDto
        {
            Id = distributor.Id,
            FirstName = distributor.FirstName,
            LastName = distributor.LastName,
            BonusAmount = distributor.BonusAmount,
        }).BuildFilterQuery(filter).ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
    }

    public async Task<int> GetDistributorRecommendationCountAsync(string distributorId)
    {
        return await Set.CountAsync(distributor => distributor.RecommenderId == distributorId);
    }
}
