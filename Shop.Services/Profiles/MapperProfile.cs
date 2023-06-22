using AutoMapper;
using ShopNT.Services.Dtos.BrandDtos;
using ShopNT.Services.Dtos.ProductDtos;
using ShopNT.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace ShopNT.Services.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile(IHttpContextAccessor accessor)
        {
            var uriBuilder = new UriBuilder(accessor.HttpContext.Request.Scheme, accessor.HttpContext.Request.Host.Host, accessor.HttpContext.Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }
            string baseUrl = uriBuilder.Uri.AbsoluteUri;
            CreateMap<Product, ProductGetItemDto>()
                .ForMember(dest=>dest.ImageUrl,m=>m.MapFrom(s=>baseUrl+$"Uploads/Products/{s.ImageName}"));
            CreateMap<Product, ProductGetAllItemsDto>()
                .ForMember(dest => dest.ImageUrl, m => m.MapFrom(s =>s.ImageName==null?null: baseUrl + $"Uploads/Products/{s.ImageName}"));
            CreateMap<ProductPostDto, Product>();
            CreateMap<Product,BrandInProductDto>();

            CreateMap<Brand,BrandGetItemDto>();
            CreateMap<Brand,BrandGetAllItemsDto>();
            CreateMap<BrandPostDto, Brand>();
        }
    }
}
