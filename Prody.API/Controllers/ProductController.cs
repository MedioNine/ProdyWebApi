using Microsoft.AspNetCore.Mvc;
using Prody.BLL.DTOs;
using Prody.BLL.ML;
using Prody.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId([FromRoute] int categoryId)
        {
            System.Collections.Generic.IEnumerable<BLL.DTOs.ReadProductDto> products =
                await _productService.GetProductsByCategoryIdAsync(categoryId);

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            BLL.DTOs.ReadProductDto product =
                await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpGet]
        [Route("getRecomendations")]
        public async Task<IActionResult> GetRecommendations([FromQuery] GetRecommendationsDto parameters)
        {
            RecommendationService service = new RecommendationService();
            //IEnumerable<ReadPrediction> predictions = service.GetPredictions(parameters);
            service.Train();
            return Ok();
        }
    }
}
