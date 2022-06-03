using Prody.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prody.DAL.Repositories.Interfaces
{
    public interface IProductRepository: IDataBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryId(int categoryId);
    }
}
