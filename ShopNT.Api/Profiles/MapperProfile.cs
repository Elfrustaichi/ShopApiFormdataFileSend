using AutoMapper;
using ShopNT.Api.Dtos.BrandDtos;
using ShopNT.Api.Dtos.ProductDtos;
using ShopNT.Core.Entities;

namespace ShopNT.Api.Profiles
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
