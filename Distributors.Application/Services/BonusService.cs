using AutoMapper;
using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Responses;
using Distributors.Core;
using Distributors.Core.Dtos;
using Distributors.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Distributors.Application.Services;
public class BonusService : IBonusService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private static readonly SemaphoreSlim _bonusCalculationLock = new(1, 1);

    public BonusService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<PaginatedObject<DistributorBonusModel>> GetDistributorBonusesAsync(GetDistributorBonusesRequest request)
    {
        var pagedList = await _unitOfWork.Distributors.GetAsync(request.Pagination, request.Filter);
        var paginatedObject = pagedList.ToPaginatedObject<DistributorBonusDto, DistributorBonusModel>(_mapper);
        return paginatedObject;
    }

    public async Task CalculateAsync(CalculateBonusRequest request)
    {
        await _bonusCalculationLock.WaitAsync(); // Lock disables the same bonus calculations by multiple requests
        try
        {
            var bonusRecommendationCoeffs = _configuration.GetSection("BonusRecommendationCoefficients").Get<List<decimal>>();
            var depth = bonusRecommendationCoeffs.Count - 1;
            var distributors = await _unitOfWork.Distributors.GetAllAsync();
            var sales = await _unitOfWork.Sales.GetAsync(request.FromDate, request.ToDate);
            foreach (var distributor in distributors)
            {
                var salesTotalPrice = sales.Where(sale => sale.DistributorId == distributor.Id).Sum(sale => sale.TotalPrice);
                var coeffIndex = 0;
                distributor.BonusAmount += salesTotalPrice * bonusRecommendationCoeffs[coeffIndex];

                var recommenders = await GetRecommendersAsync(distributor.Id, depth);
                foreach (var recommender in recommenders)
                {
                    coeffIndex++;
                    recommender.BonusAmount += salesTotalPrice * bonusRecommendationCoeffs[coeffIndex];
                }
            }

            sales.ForEach(sale => sale.IsBonusCalculated = true);
            await _unitOfWork.CompleteAsync();
        }
        finally
        {
            _bonusCalculationLock.Release();
        }
    }

    private async Task<List<DistributorEntity>> GetRecommendersAsync(string distributorId, int depth)
    {
        var recommenders = new List<DistributorEntity>();
        var recomendationDepth = 0;
        var distributor = (await _unitOfWork.Distributors.GetByPrimaryKeyAsync(distributorId))!;
        if (distributor.RecommenderId is null)
        {
            return recommenders;
        }
        var nextDistributorId = distributor.RecommenderId;

        while (recomendationDepth < depth)
        {
            var currentDistributor = await _unitOfWork.Distributors.GetByPrimaryKeyAsync(nextDistributorId);

            if (currentDistributor != null)
                recomendationDepth++;
            else
                break;

            nextDistributorId = currentDistributor.RecommenderId;
            recommenders.Add(currentDistributor);
        }

        return recommenders;
    }
}
