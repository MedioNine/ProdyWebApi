using Prody.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prody.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ReadProductDto>> GetProductsByCategoryIdAsync(int categoryId);
        Task<ReadProductDto> GetProductById(int productId);
    }
}
