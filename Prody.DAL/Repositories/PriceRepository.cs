using Microsoft.EntityFrameworkCore;
using Prody.DAL.Entities;
using Prody.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prody.DAL.Repositories
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        private readonly ProdyDbContext _context;

        public PriceRepository(ProdyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Price>> GetByProductId(int productId)
        {
            return await _context.Prices
                .Where(p => p.ProductId == productId)
                .ToListAsync();
        }
    }
}