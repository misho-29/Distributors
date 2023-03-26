using Distributors.Core.Entities;
using Distributors.Core.Repositories;
using Distributors.Infrastructure.Data;

namespace Distributors.Infrastructure.Repositories;
public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(DistributorsDbContext context) : base(context)
    {
    }
}
