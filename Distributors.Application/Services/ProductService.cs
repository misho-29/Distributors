using AutoMapper;
using Distributors.Application.Models.Requests;
using Distributors.Core;
using Distributors.Core.Entities;
using Distributors.Core.Exceptions;

namespace Distributors.Application.Services;
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddAsync(AddProductRequest request)
    {
        var productExists = await _unitOfWork.Products.ExistsByPrimaryKeyAsync(request.Code);
        if (productExists)
        {
            throw new ValidationException(ExceptionCode.ProductCodeAlreadyExists);
        }

        var product = _mapper.Map<ProductEntity>(request);
        await _unitOfWork.Products.InsertAsync(product);
        await _unitOfWork.CompleteAsync();
    }
}
