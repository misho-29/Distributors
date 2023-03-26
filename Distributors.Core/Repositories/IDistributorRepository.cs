using Distributors.Core.DisplayTools.Filter.Models;
using Distributors.Core.DisplayTools.Pagination;
using Distributors.Core.Dtos;
using Distributors.Core.Entities;
using X.PagedList;

namespace Distributors.Core.Repositories;
public interface IDistributorRepository : IGenericRepository<DistributorEntity>
{
    Task<IPagedList<DistributorEntity>> GetAsync(PaginationObj pagination);
    Task<bool> IsDistributorRecommender(string distributorId);
    Task<IPagedList<DistributorBonusDto>> GetAsync(PaginationObj pagination, Filter? filter);
    Task<int> GetDistributorRecommendationCountAsync(string distributorId);
}
