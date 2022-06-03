using Prody.DAL.Entities;
using Prody.DAL.Repositories;
using Prody.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Prody.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProdyDbContext _context;
        private Repository<Category> _categoryRepository;
        private Repository<ShoppingList> _shoppingRepository;
        private ProductRepository _productRepository;
        private PriceRepository _priceRepository;

        public UnitOfWork(ProdyDbContext context)
        {
            _context = context;
        }

        public IDataBaseRepository<Category> Categories => _categoryRepository = _categoryRepository ?? new Repository<Category>(_context);

        public IDataBaseRepository<ShoppingList> ShoppingList => _shoppingRepository = _shoppingRepository ?? new Repository<ShoppingList>(_context);

        public IPriceRepository Prices => _priceRepository = _priceRepository ?? new PriceRepository(_context);

        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
