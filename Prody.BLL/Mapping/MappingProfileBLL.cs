using AutoMapper;
using Prody.BLL.DTOs;
using Prody.DAL.Entities;
using Prody.Rest.Contracts.Models.Silpo;

namespace Prody.BLL.Mapping
{
    public class MappingProfileBLL : Profile
    {
        public MappingProfileBLL()
        {
            CreateMap<SilpoCategory, Category>()
                .ReverseMap();

            CreateMap<SilpoProduct, Product>()
                .ReverseMap();

            CreateMap<Category, ReadCategoryDto>()
                .ReverseMap();

            CreateMap<Product, ReadProductDto>()
                .ReverseMap();

            CreateMap<Price, ReadPriceDto>()
                .ReverseMap();
        }
    }
}
