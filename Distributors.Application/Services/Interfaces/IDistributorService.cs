using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Requests.AddDistributor;
using Distributors.Application.Models.Responses;
using Distributors.Core.DisplayTools.Pagination;

namespace Distributors.Application.Services;
public interface IDistributorService
{
    Task UpdateAsync(string distributorId, UpdateDistributorRequest request);
    Task<PaginatedObject<DistributorModel>> GetAsync(PaginationObj pagination);
    Task DeleteAsync(string distributorId);
    Task AddAsync(AddDistributorRequest request);
}
