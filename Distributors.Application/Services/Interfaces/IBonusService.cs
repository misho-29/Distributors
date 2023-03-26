using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Responses;

namespace Distributors.Application.Services;
public interface IBonusService
{
    Task<PaginatedObject<DistributorBonusModel>> GetDistributorBonusesAsync(GetDistributorBonusesRequest request);
    Task CalculateAsync(CalculateBonusRequest request);
}
