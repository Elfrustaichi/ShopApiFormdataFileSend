using AutoMapper;
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
        private readonly IMapper _mapper;

        public BrandsController(IBrandRepository brandRepository,IMapper mapper) 
        {
            
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        [HttpGet("get/{id}")]
        public ActionResult<BrandGetItemDto> Get(int id)
        {
            var data = _brandRepository.Get(x => x.Id == id);

            if (data == null) return NotFound();

            BrandGetItemDto brand = _mapper.Map<BrandGetItemDto>(data);

            return StatusCode(200, brand);
        }
        [HttpPost("create")]
        public IActionResult Create(BrandPostDto brandPostDto)
        {
            Brand brand = _mapper.Map<Brand>(brandPostDto);

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
            var data = _mapper.Map<List<BrandGetAllItemsDto>>(_brandRepository.GetAll(x=>true));

            return Ok(data);

           
        }
    }
}
