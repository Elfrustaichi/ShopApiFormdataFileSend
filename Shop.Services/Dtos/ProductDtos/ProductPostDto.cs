using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace ShopNT.Services.Dtos.ProductDtos
{
    public class ProductPostDto
    {
        public string Name { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public decimal DiscountPercent { get; set; }

        public IFormFile ImageFile { get; set; }
        public int BrandId { get; set; }
    }

    public class ProductPostValidator:AbstractValidator<ProductPostDto>
    {
        public ProductPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).WithMessage("Name lenght must below 20");
            RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(x => x.CostPrice);
            RuleFor(x => x.CostPrice).GreaterThan(0);
            RuleFor(x=>x.DiscountPercent).GreaterThan(0).LessThanOrEqualTo(100);


            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.DiscountPercent > 0)
                {
                    var price = x.SalePrice * (100 - x.DiscountPercent) / 100;
                    if(x.CostPrice > price)
                    {
                        context.AddFailure(nameof(x.DiscountPercent),"Discount percent is to high!");
                    }
                }
            });
        }
    }
}
