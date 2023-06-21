using ShopNT.Services.Dtos.BrandDtos;
using ShopNT.Services.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNT.Services.Interfaces
{
    public interface IBrandService
    {
        CreatedEntityDto Create(BrandPostDto postDto);

        void Edit(int id,BrandPutDto putDto);

        List<BrandGetAllItemsDto> GetAll();

        void Delete(int id);

        BrandGetItemDto Get(int id);
    }
}
