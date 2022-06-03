using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prody.BLL.Services.Interfaces;
using System.Threading.Tasks;

namespace Prody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrabberController : ControllerBase
    {
        private readonly ISilpoService _silpoService;
        private readonly IMapper _mapper;
        public GrabberController(ISilpoService silpoService)
        {
            _silpoService = silpoService;
        }

        [HttpPost]
        [Route("updateCategories")]
        public async Task<ActionResult> UpdateCategories()
        {
            await _silpoService.GrabCategoriesFromSilpo();

            return Ok();
        }

        [HttpPost]
        [Route("updateProducts")]
        public async Task<ActionResult> UpdateProducts()
        {
            await _silpoService.GrabProductsFromSilpoByCategory();

            return Ok();
        }
    }
}
