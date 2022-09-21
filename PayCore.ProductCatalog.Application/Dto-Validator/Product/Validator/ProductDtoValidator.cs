using FluentValidation;
using PayCore.ProductCatalog.Application.Dto_Validator.Product.Dto;


namespace PayCore.ProductCatalog.Application.Dto_Validator.Product.Validator
{
    public class ProductDtoValidator:AbstractValidator<ProductUpsertDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(p => p.ProductName).MaximumLength(100).NotEmpty();
            RuleFor(p => p.Description).MaximumLength(500).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0).NotEmpty();
        }
    }
}
