
using FluentValidation;


namespace PayCore.ProductCatalog.Application.Dto_Validator
{
    public class OfferDtoValidator:AbstractValidator<OfferUpsertDto>
    {
        public OfferDtoValidator()
        {
            RuleFor(x => x.OfferedPrice).GreaterThan(0);
            RuleFor(x => x.ProductId).GreaterThan(0);
        }
    }
}
