using Distributors.Core.Repositories;

namespace Distributors.Core;
public interface IUnitOfWork
{
    IDistributorRepository Distributors { get; }
    IProductRepository Products { get; }
    ISaleRepository Sales { get; }
    IIdentityDocumentRepository IdentityDocuments { get; }

    Task<int> CompleteAsync();
}
