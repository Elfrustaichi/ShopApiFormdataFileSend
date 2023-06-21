using AutoMapper;
using ShopNT.Services.Dtos.BrandDtos;
using ShopNT.Services.Dtos.ProductDtos;
using ShopNT.Core.Entities;

namespace ShopNT.Services.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductGetItemDto>();
            CreateMap<Product,ProductGetAllItemsDto>();
            CreateMap<ProductPostDto, Product>();
            CreateMap<Product,BrandInProductDto>();

            CreateMap<Brand,BrandGetItemDto>();
            CreateMap<Brand,BrandGetAllItemsDto>();
            CreateMap<BrandPostDto, Brand>();
        }
    }
}
