using Prody.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prody.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<ReadCategoryDto>> GetCategoriesAsync();
        Task<ReadCategoryDto> GetCategoryById(int categoryId);
    }
}
