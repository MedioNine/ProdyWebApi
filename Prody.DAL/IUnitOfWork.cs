using Prody.DAL.Entities;
using Prody.DAL.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Prody.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IDataBaseRepository<Category> Categories { get; }
        IProductRepository Products { get; }
        IPriceRepository Prices { get; }
        IDataBaseRepository<ShoppingList> ShoppingList { get; }
        Task<int> CommitAsync();
    }
}
