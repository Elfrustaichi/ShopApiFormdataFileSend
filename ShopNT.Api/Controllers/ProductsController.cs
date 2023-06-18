using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopNT.Api.Dtos.ProductDtos;
using ShopNT.Api.Helpers;
using ShopNT.Core.Entities;
using ShopNT.Core.Repositories;


namespace ShopNT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IWebHostEnvironment _env;

        public ProductsController(IProductRepository productRepository,IBrandRepository brandRepository,IWebHostEnvironment env)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _env = env;
        }
        [HttpPost("create")]
        public ActionResult<ProductPostDto> Create([FromForm]ProductPostDto productPostDto)
        {
            Product product = new Product
            {
                Name = productPostDto.Name,
                CostPrice = productPostDto.CostPrice,
                SalePrice = productPostDto.SalePrice,
                DiscountPercent = productPostDto.DiscountPercent,
                ImageName = FileManager.Save(_env.WebRootPath, "Uploads/Products", productPostDto.ImageFile)
            };

            if (!_brandRepository.IsExist(x => x.Id == productPostDto.BrandId))
            {
                ModelState.AddModelError("BrandId", "Brand cannot found!");
                return BadRequest(ModelState);
            }
            else
            {
                product.BrandId= productPostDto.BrandId;
                _productRepository.Add(product);
                _productRepository.Commit();

                return Ok(product);
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
            if(string.IsNullOrEmpty(productPutDto.Name))
            {
                product.Name = productPutDto.Name;
            }
            if (productPutDto.CostPrice > 0)
            {
                product.CostPrice = productPutDto.CostPrice;
            }
            if(productPutDto.SalePrice>0) product.SalePrice = productPutDto.SalePrice;

            if(productPutDto.DiscountPercent>0) product.DiscountPercent = productPutDto.DiscountPercent;

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
            var data=_productRepository.Get(x=>x.Id == id,"Brand");

            if (data == null) return NotFound();

            ProductGetItemDto productGetItemDto = new ProductGetItemDto
            {
                Name = data.Name,
                SalePrice = data.SalePrice,
                DiscountPercent = data.DiscountPercent,
                CostPrice = data.CostPrice,
                ImageName = data.ImageName,
                brandinproduct=new BrandInProductDto { Name=data.Brand.Name},

                
            };

            return Ok(productGetItemDto);
        }
        [HttpGet("GetAll")]
        public ActionResult<List<ProductGetAllItemsDto>> GetAll()
        {
            var data = _productRepository.GetAllQueryable(x=>true,"Brand").Select(x=>new ProductGetAllItemsDto { Name=x.Name,SalePrice=x.SalePrice,CostPrice=x.CostPrice,DiscountPercent=x.DiscountPercent,ImageName=x.ImageName,brandinproduct=new BrandInProductDto { Name=x.Brand.Name} });

            return Ok(data);
        }
    }
}
