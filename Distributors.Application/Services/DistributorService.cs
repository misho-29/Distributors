using AutoMapper;
using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Requests.AddDistributor;
using Distributors.Application.Models.Responses;
using Distributors.Core;
using Distributors.Core.DisplayTools.Pagination;
using Distributors.Core.Entities;
using Distributors.Core.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Distributors.Application.Services;
public class DistributorService : IDistributorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public DistributorService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task UpdateAsync(string distributorId, UpdateDistributorRequest request)
    {
        var distributor = await _unitOfWork.Distributors.GetByPrimaryKeyAsync(distributorId);

        if (distributor is null)
        {
            throw new ValidationException(ExceptionCode.DistributorNotFound);
        }

        _mapper.Map(request, distributor);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<PaginatedObject<DistributorModel>> GetAsync(PaginationObj pagination)
    {
        var pagedList = await _unitOfWork.Distributors.GetAsync(pagination);
        var paginatedObject = pagedList.ToPaginatedObject<DistributorEntity, DistributorModel>(_mapper);
        return paginatedObject;
    }

    public async Task DeleteAsync(string distributorId)
    {
        var distributor = await _unitOfWork.Distributors.GetByPrimaryKeyAsync(distributorId);

        if (distributor is null)
        {
            throw new ValidationException(ExceptionCode.DistributorNotFound);
        }

        if (await _unitOfWork.Distributors.IsDistributorRecommender(distributorId))
        {
            throw new ValidationException(ExceptionCode.DistributorIsRecommender);
        }

        _unitOfWork.Distributors.Delete(distributor);
        await _unitOfWork.CompleteAsync();
    }

    public async Task AddAsync(AddDistributorRequest request)
    {
        if (request.RecommenderId is not null)
        {
            var recommenderExists = await _unitOfWork.Distributors.ExistsByPrimaryKeyAsync(request.RecommenderId);

            if (!recommenderExists)
            {
                throw new ValidationException(ExceptionCode.RecommenderNotExists);
            }

            var maxRecommendationCount = int.Parse(_configuration["MaxRecommendationCount"] ?? "3");
            var recommendationCount = await _unitOfWork.Distributors.GetDistributorRecommendationCountAsync(request.RecommenderId);
            if (recommendationCount >= maxRecommendationCount)
            {
                throw new ValidationException(ExceptionCode.RecommenderExceededRecommendationCount);
            }

            var maxRecommendationDepth = int.Parse(_configuration["MaxRecommendationDepth"] ?? "5");
            var recommendationDepth = await GetDistributorRecommendationDepthAsync(request.RecommenderId);
            if (recommendationDepth >= maxRecommendationDepth)
            {
                throw new ValidationException(ExceptionCode.RecommendationDepthExceeded);
            }
        }

        var distributorEntity = _mapper.Map<DistributorEntity>(request);
        await _unitOfWork.Distributors.InsertAsync(distributorEntity);
        await _unitOfWork.CompleteAsync();
    }

    private async Task<int> GetDistributorRecommendationDepthAsync(string distributorId)
    {
        var recomendationDepth = 0;
        var nextDistributorId = distributorId;

        while (true)
        {
            var currentDistributor = await _unitOfWork.Distributors.GetByPrimaryKeyAsync(nextDistributorId);

            if (currentDistributor != null)
                recomendationDepth++;
            else
                return recomendationDepth;

            nextDistributorId = currentDistributor.RecommenderId;
        }
    }
}
