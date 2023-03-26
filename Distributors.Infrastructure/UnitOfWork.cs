using Distributors.Core;
using Distributors.Core.Repositories;
using Distributors.Infrastructure.Data;
using Distributors.Infrastructure.Repositories;

namespace Distributors.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly DistributorsDbContext _context;
    public UnitOfWork(DistributorsDbContext context)
    {
        _context = context;
    }

    private IDistributorRepository? _distributorRepository;
    public IDistributorRepository Distributors => _distributorRepository ??= new DistributorRepository(_context);

    private IProductRepository? _productRepository;
    public IProductRepository Products => _productRepository ??= new ProductRepository(_context);

    private ISaleRepository? _saleRepository;
    public ISaleRepository Sales => _saleRepository ??= new SaleRepository(_context);

    private IIdentityDocumentRepository? _identityDocumentRepository;
    public IIdentityDocumentRepository IdentityDocuments => _identityDocumentRepository ??= new IdentityDocumentRepository(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private bool _isDisposed;
    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed && disposing)
        {
            _context.Dispose();
        }
        _isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
