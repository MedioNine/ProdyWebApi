using AutoMapper;
using Prody.BLL.DTOs;
using Prody.BLL.Services.Interfaces;
using Prody.DAL;
using Prody.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prody.BLL.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadCategoryDto>> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadCategoryDto>>(categories);
        }

        public async Task<ReadCategoryDto> GetCategoryById(int categoryId)
        {
            Category category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            return _mapper.Map<ReadCategoryDto>(category);
        }
    }
}
