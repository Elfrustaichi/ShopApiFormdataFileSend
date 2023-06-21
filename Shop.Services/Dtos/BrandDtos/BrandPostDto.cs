using FluentValidation;

namespace ShopNT.Services.Dtos.BrandDtos
{
    public class BrandPostDto
    {
        public string Name { get; set; }
    }

    public class BrandPostDtoValidator : AbstractValidator<BrandPostDto>
    {
        public BrandPostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name cannot be empty").MaximumLength(20).WithMessage("Lenght must be lower than 20");
        }

    }
}
