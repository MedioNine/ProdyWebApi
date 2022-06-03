using AutoMapper;
using Prody.BLL.Services.Interfaces;
using Prody.DAL;
using Prody.DAL.Entities;
using Prody.Rest.Contracts.Models.Silpo;
using Prody.Rest.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prody.BLL.Services
{
    public class SilpoService : ISilpoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISilpo _silpoApi;
        private readonly IMapper _mapper;
        private readonly Random _random;
        public SilpoService(IUnitOfWork unitOfWork, ISilpo silpoApi, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _silpoApi = silpoApi;
            _mapper = mapper;
            _random = new Random();
        }

        public async Task GrabCategoriesFromSilpo()
        {
            SilpoCategories result = await _silpoApi.GetCategories();

            if (result.Categories.Any())
            {
                List<SilpoCategory> globalCategories = result.Categories.FindAll(category => category.ParentId == null);
                foreach (SilpoCategory category in globalCategories)
                {
                    Category categoryToUpdate = await _unitOfWork.Categories.GetByIdAsync(category.Id);
                    if (categoryToUpdate != null)
                    {
                        Category categoryMapped = _mapper.Map(category, categoryToUpdate);
                        _unitOfWork.Categories.Update(categoryMapped);
                    }
                    else
                    {
                        Category newCategory = _mapper.Map<SilpoCategory, Category>(category);
                        await _unitOfWork.Categories.AddAsync(newCategory);
                    }
                }
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task GrabProductsFromSilpoByCategory()
        {
            IEnumerable<Category> categories = await _unitOfWork.Categories.GetAllAsync();

            foreach (Category category in categories)
            {
                int from, to = 1;
                int step = 1000;

                while (to < category.Items)
                {
                    from = to;
                    to += step;

                    SilpoGetByCategoryResponse result = await _silpoApi.GetProductsByCategory(category.Id, from, Math.Min(category.Items, to));

                    if (result.Items.Any())
                    {
                        foreach (SilpoProduct product in result.Items)
                        {
                            Product productToUpdate = await _unitOfWork.Products.GetByIdAsync(product.Id);
                            if (productToUpdate != null)
                            {
                                Product productMapped = _mapper.Map(product, productToUpdate);
                                _unitOfWork.Products.Update(productMapped);
                                IEnumerable<Price> prices = await _unitOfWork.Prices.GetByProductId(product.Id);
                                foreach (Price price in prices)
                                {
                                    if (price.Seller == "Silpo")
                                    {
                                        price.Value = product.Price;
                                    }
                                    else
                                    {
                                        price.Value = getPriceAround(product.Price);
                                    }
                                    _unitOfWork.Prices.Update(price);
                                }
                            }
                            else
                            {
                                Product newProduct = _mapper.Map<SilpoProduct, Product>(product);
                                newProduct.CategoryId = category.Id;
                                await _unitOfWork.Products.AddAsync(newProduct);
                                Price silpoPrice = new Price { ProductId = newProduct.Id, Value = product.Price, Seller = "Silpo" };
                                await _unitOfWork.Prices.AddAsync(silpoPrice);

                                // ATB
                                Price atbPrice = new Price { ProductId = newProduct.Id, Value = getPriceAround(product.Price), Seller = "ATB" };
                                await _unitOfWork.Prices.AddAsync(atbPrice);

                                // Auchan
                                Price auchanPrice = new Price { ProductId = newProduct.Id, Value = getPriceAround(product.Price), Seller = "Auchan" };
                                await _unitOfWork.Prices.AddAsync(auchanPrice);
                            }
                        }
                    }

                    await _unitOfWork.CommitAsync();
                }
            }
        }

        private float getPriceAround(float price)
        {
            return (float)(price * (1 + (_random.NextDouble() - 0.5) / 3));
        }
    }
}
