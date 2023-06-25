using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ShopNT.Services.Dtos.ProductDtos;
using ShopNT.Core.Entities;
using ShopNT.Core.Repositories;
using ShopNT.Services.Interfaces;

namespace ShopNT.Services.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("create")]
        public ActionResult<ProductPostDto> Create([FromForm]ProductPostDto productPostDto)
        {

            return StatusCode(201, _productService.Create(productPostDto));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public ActionResult<ProductPutDto> Edit(int id,[FromForm]ProductPutDto productPutDto)
        {
            _productService.Edit(id, productPutDto);
            
            return NoContent();
        }
        [HttpGet("get/{id}")]
        public ActionResult<ProductGetItemDto> Get(int id)
        {
            return Ok(_productService.GetById(id));
        }
        [HttpGet("GetAll")]
        public ActionResult<List<ProductGetAllItemsDto>> GetAll()
        {

            return Ok(_productService.GetAll());
        }
    }
}
