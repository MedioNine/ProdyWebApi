using Prody.Rest.Contracts.Models.Silpo;
using System.Threading.Tasks;

namespace Prody.Rest.Controllers.Interfaces
{
    public interface ISilpo
    {
        Task<SilpoGetByCategoryResponse> GetProductsByCategory(int categoryId, int from, int to);

        Task<SilpoCategories> GetCategories();
    }
}
