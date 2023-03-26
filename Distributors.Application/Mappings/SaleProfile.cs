using AutoMapper;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Responses.GetSales;
using Distributors.Core.Entities;

namespace Distributors.Application.Mappings;
public class SaleProfile : Profile
{
    public SaleProfile()
    {
        CreateMap<AddSaleRequest, SaleEntity>();
        CreateMap<Core.Dtos.GetSales.SaleDto, SaleModel>();
    }
}
