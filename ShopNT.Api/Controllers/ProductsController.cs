using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ShopNT.Services.Dtos.ProductDtos;
using ShopNT.Services.Helpers;
using ShopNT.Core.Entities;
using ShopNT.Core.Repositories;


namespace ShopNT.Services.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,IBrandRepository brandRepository,IWebHostEnvironment env,IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _env = env;
            _mapper = mapper;
        }
        [HttpPost("create")]
        public ActionResult<ProductPostDto> Create([FromForm]ProductPostDto productPostDto)
        {
            var entity=_mapper.Map<Product>(productPostDto);

            if (!_brandRepository.IsExist(x => x.Id == productPostDto.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand cannot found!");
                return BadRequest(ModelState);
            }
            else
            {
                entity.BrandId= productPostDto.BrandId;
                _productRepository.Add(entity);
                _productRepository.Commit();

                return Ok(entity);
            }

            

        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var product=_productRepository.Get(x=>x.Id == id);

            if (product == null) return NotFound();

            _productRepository.Delete(product);
            _productRepository.Commit();

            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public ActionResult<ProductPutDto> Edit(int id,[FromForm]ProductPutDto productPutDto)
        {
            var product = _productRepository.Get(x => x.Id == id);

            

            if (product == null) return NotFound();
            
             product.Name = productPutDto.Name;
             product.CostPrice = productPutDto.CostPrice;
             product.SalePrice = productPutDto.SalePrice;
             product.DiscountPercent = productPutDto.DiscountPercent;

            if (productPutDto.ImageFile !=null)
            {
                var OldImageName = product.ImageName;
                product.ImageName = FileManager.Save(_env.WebRootPath, "Uploads/Products", productPutDto.ImageFile);
                FileManager.Delete(_env.WebRootPath, "Uploads/Product", OldImageName);
            }
            


            if(!_brandRepository.IsExist(x=>x.Id==productPutDto.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand cannot found");
                return BadRequest(ModelState);
            }
            else
            {
                product.BrandId = productPutDto.BrandId;
                _productRepository.Commit();

                return NoContent();
            }
            
        }
        [HttpGet("get/{id}")]
        public ActionResult<ProductGetItemDto> Get(int id)
        {
            var entity=_productRepository.Get(x=>x.Id == id,"Brand");

            if (entity == null) return NotFound();

           var data=_mapper.Map<ProductGetItemDto>(entity);

            return Ok(data);
        }
        [HttpGet("GetAll")]
        public ActionResult<List<ProductGetAllItemsDto>> GetAll()
        {
            
            var data = _mapper.Map<List<ProductGetAllItemsDto>>(_productRepository.GetAll(x => true, "Brand"));

            return Ok(data);
        }
    }
}
