using System.Threading.Tasks;

namespace Prody.BLL.Services.Interfaces
{
    public interface IShoppingService
    {
        Task AddShoppingList(int[] productIds);
    }
}
