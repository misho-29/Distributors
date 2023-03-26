using AutoMapper;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Responses.GetSales;
using Distributors.Core.Entities;

namespace Distributors.Application.Mappings;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductRequest, ProductEntity>();
        CreateMap<Core.Dtos.GetSales.Product, Product>();
    }
}
