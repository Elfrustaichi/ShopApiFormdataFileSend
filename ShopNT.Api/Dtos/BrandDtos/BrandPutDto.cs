using FluentValidation;

namespace ShopNT.Api.Dtos.BrandDtos
{
    public class BrandPutDto
    {
        public string Name { get; set; }
    }

    public class BrandPutDtoValidator : AbstractValidator<BrandPutDto>
    {
        public BrandPutDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name cannot be empty").MaximumLength(20).WithMessage("Lenght must be lower than 20");
        }
    }
}
