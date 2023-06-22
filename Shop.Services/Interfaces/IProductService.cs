using ShopNT.Services.Dtos.Common;
using ShopNT.Services.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNT.Services.Interfaces
{
    public interface IProductService
    {
        CreatedEntityDto Create(ProductPostDto dto);
        void Edit(int id, ProductPutDto dto);
        ProductGetItemDto GetById(int id);
        List<ProductGetAllItemsDto> GetAll();
        void Delete(int id);
    }
}
