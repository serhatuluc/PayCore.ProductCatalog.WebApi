using FluentValidation;


namespace PayCore.ProductCatalog.Application.Dto_Validator
{
    public class BrandDtoValidator:AbstractValidator<BrandUpsertDto>
    {
        public BrandDtoValidator()
        {
            RuleFor(x => x.BrandName).NotEmpty().WithMessage("Brand can not be null");
        }
    }
}
