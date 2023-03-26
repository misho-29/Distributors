using AutoMapper;
using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Responses.GetSales;
using Distributors.Core;
using Distributors.Core.Dtos.GetSales;
using Distributors.Core.Entities;
using Distributors.Core.Exceptions;

namespace Distributors.Application.Services;
public class SaleService : ISaleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedObject<SaleModel>> GetAsync(GetSalesRequest request)
    {
        var pagedList = await _unitOfWork.Sales.GetAsync(request.Pagination, request.Filter);
        var paginatedObject = pagedList.ToPaginatedObject<SaleDto, SaleModel>(_mapper);
        return paginatedObject;
    }

    public async Task AddAsync(AddSaleRequest request)
    {
        var distributorExists = await _unitOfWork.Distributors.ExistsByPrimaryKeyAsync(request.DistributorId);
        if (!distributorExists)
        {
            throw new ValidationException(ExceptionCode.DistributorNotFound);
        }

        var product = await _unitOfWork.Products.GetByPrimaryKeyAsync(request.ProductCode);
        if (product is null)
        {
            throw new ValidationException(ExceptionCode.ProductNotFound);
        }

        var saleEntity = _mapper.Map<SaleEntity>(request);
        saleEntity.ProductCurrentPrice = product.Price;
        await _unitOfWork.Sales.InsertAsync(saleEntity);
        await _unitOfWork.CompleteAsync();
    }
}
