using Microsoft.AspNetCore.Mvc;
using Prody.BLL.Services.Interfaces;
using System.Threading.Tasks;

namespace Prody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingHistoryController : ControllerBase
    {
        private readonly IShoppingService _shoppingService;

        public ShoppingHistoryController(IShoppingService shoppingService)
        {
            _shoppingService = shoppingService;
        }

        [HttpPost]
        public async Task<IActionResult> GetByCategoryId([FromBody] int[] productsIds)
        {
            await _shoppingService.AddShoppingList(productsIds);

            return Ok();
        }
    }
}
