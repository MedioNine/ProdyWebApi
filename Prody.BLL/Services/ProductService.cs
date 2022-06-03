using AutoMapper;
using Prody.BLL.DTOs;
using Prody.BLL.Services.Interfaces;
using Prody.DAL;
using Prody.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prody.BLL.Services
{
    public class ProductService: IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadProductDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            IEnumerable<Product> products = await _unitOfWork.Products.GetByCategoryId(categoryId);
            return _mapper.Map<IEnumerable<ReadProductDto>>(products);
        }

        public async Task<ReadProductDto> GetProductById(int productId)
        {
            Product product = await _unitOfWork.Products.GetByIdAsync(productId);
            return _mapper.Map<ReadProductDto>(product);
        }
    }
}
