using Microsoft.EntityFrameworkCore;
using Prody.DAL.Entities;
using Prody.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prody.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ProdyDbContext _context;

        public ProductRepository(ProdyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCategoryId(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Include(p => p.Prices)
                .ToListAsync();
        }
        public override async ValueTask<Product> GetByIdAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Prices)
                .SingleOrDefaultAsync(p => p.Id == productId);
        }
    }
}
