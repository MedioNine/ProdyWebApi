using Prody.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prody.DAL.Repositories.Interfaces
{
    public interface IPriceRepository : IDataBaseRepository<Price>
    {
        Task<IEnumerable<Price>> GetByProductId(int productId);
    }
}
