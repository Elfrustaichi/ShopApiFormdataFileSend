using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopNT.Api.Dtos.BrandDtos;
using ShopNT.Core.Entities;
using ShopNT.Core.Repositories;
using ShopNT.Data;

namespace ShopNT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
       
        private readonly IBrandRepository _brandRepository;

        public BrandsController(IBrandRepository brandRepository) 
        {
            
            _brandRepository = brandRepository;
        }
        [HttpGet("get/{id}")]
        public ActionResult<BrandGetItemDto> Get(int id)
        {
            var data = _brandRepository.Get(x => x.Id == id);

            if (data == null) return NotFound();

            BrandGetItemDto brand = new BrandGetItemDto
            {
                Name = data.Name,
            };

            return StatusCode(200, brand);
        }
        [HttpPost("create")]
        public IActionResult Create(BrandPostDto brandPostDto)
        {
            Brand brand = new Brand
            {
                Name = brandPostDto.Name,
            };
            _brandRepository.Add(brand);
            _brandRepository.Commit();

            return StatusCode(201, new {brand.Id});
        }

        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id,BrandPutDto brandPutDto)
        {
            Brand brand = _brandRepository.Get(x=>x.Id==id);

            if(brand == null)
            {
                return NotFound();
            }

            brand.Name = brandPutDto.Name;
            _brandRepository.Commit();

            return NoContent();
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Brand brand =_brandRepository.Get(x => x.Id == id);

            if (brand == null) { return NotFound(); }

            _brandRepository.Delete(brand);
            _brandRepository.Commit();

            return NoContent();

        }

        [HttpGet("GetAll")]
        public ActionResult<List<BrandGetAllItemsDto>> GetAll()
        {
            var data = _brandRepository.GetAllQueryable(x=>true).Select(x =>  new BrandGetAllItemsDto {Name=x.Name,Id=x.Id}).ToList();

            return Ok(data);

           
        }
    }
}
