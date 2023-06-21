using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopNT.Services.Interfaces;
using ShopNT.Services.Dtos.BrandDtos;
using ShopNT.Core.Entities;
using ShopNT.Core.Repositories;
using ShopNT.Data;

namespace ShopNT.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService) 
        {
            _brandService = brandService;
        }
        [HttpGet("get/{id}")]
        public ActionResult<BrandGetItemDto> Get(int id)
        {
            var result=_brandService.Get(id);

            return StatusCode(200, result);
        }
        [HttpPost("create")]
        public IActionResult Create(BrandPostDto brandPostDto)
        {
            var result=_brandService.Create(brandPostDto);

            return StatusCode(201,result);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id,BrandPutDto brandPutDto)
        {
            _brandService.Edit(id,brandPutDto);

            return NoContent();
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _brandService.Delete(id);

            return NoContent();

        }

        [HttpGet("GetAll")]
        public ActionResult<List<BrandGetAllItemsDto>> GetAll()
        {
            

            return Ok(_brandService.GetAll());

           
        }
    }
}
