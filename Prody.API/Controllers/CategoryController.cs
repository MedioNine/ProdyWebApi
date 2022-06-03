using Microsoft.AspNetCore.Mvc;
using Prody.BLL.DTOs;
using Prody.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ReadCategoryDto> categories =
                await _categoryService.GetCategoriesAsync();

            return Ok(categories);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            ReadCategoryDto category =
                await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }
    }
}
