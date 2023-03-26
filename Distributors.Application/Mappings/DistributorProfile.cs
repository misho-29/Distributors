using AutoMapper;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Requests.AddDistributor;
using Distributors.Application.Models.Responses;
using Distributors.Application.Models.Responses.GetSales;
using Distributors.Core.Dtos;
using Distributors.Core.Entities;

namespace Distributors.Application.Mappings;

public class DistributorProfile : Profile
{
    public DistributorProfile()
    {
        CreateMap<AddDistributorRequest, DistributorEntity>();
        CreateMap<DistributorEntity, DistributorModel>();
        CreateMap<UpdateDistributorRequest, DistributorEntity>();
        CreateMap<Core.Dtos.GetSales.Distributor, Distributor>();
        CreateMap<DistributorBonusDto, DistributorBonusModel>();
    }
}
