using AutoMapper;
using Prody.BLL.Services.Interfaces;
using Prody.DAL;
using Prody.DAL.Entities;
using System.Threading.Tasks;

namespace Prody.BLL.Services
{
    public class ShoppingService : IShoppingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ShoppingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddShoppingList(int[] productIds)
        {
            ShoppingList newShoppingList = new ShoppingList { ProductList = string.Join(",", productIds) };
            await _unitOfWork.ShoppingList.AddAsync(newShoppingList);
            await _unitOfWork.CommitAsync();
        }
    }
}
