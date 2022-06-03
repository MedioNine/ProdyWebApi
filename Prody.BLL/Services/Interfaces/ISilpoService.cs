using System.Threading.Tasks;

namespace Prody.BLL.Services.Interfaces
{
    public interface ISilpoService
    {
        Task GrabCategoriesFromSilpo();

        Task GrabProductsFromSilpoByCategory();
    }
}
