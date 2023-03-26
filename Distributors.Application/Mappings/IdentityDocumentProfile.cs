using AutoMapper;
using Distributors.Application.Models.Requests.AddDistributor;
using Distributors.Core.Entities;

namespace Distributors.Application.Mappings;
public class IdentityDocumentProfile : Profile
{
    public IdentityDocumentProfile()
    {
        CreateMap<IdentityDocument, IdentityDocumentEntity>();
    }
}
