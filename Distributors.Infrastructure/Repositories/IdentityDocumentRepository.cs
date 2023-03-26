using Distributors.Core.Entities;
using Distributors.Core.Repositories;
using Distributors.Infrastructure.Data;

namespace Distributors.Infrastructure.Repositories;
public class IdentityDocumentRepository : GenericRepository<IdentityDocumentEntity>, IIdentityDocumentRepository
{
    public IdentityDocumentRepository(DistributorsDbContext context) : base(context)
    {
    }
}
