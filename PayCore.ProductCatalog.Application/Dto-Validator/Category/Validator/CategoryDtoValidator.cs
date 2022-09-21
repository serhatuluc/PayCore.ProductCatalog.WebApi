using FluentValidation;


namespace PayCore.ProductCatalog.Application.Dto_Validator
{
    public class CategoryDtoValidator : AbstractValidator<CategoryUpsertDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category can not be null");
        }
    }
}
