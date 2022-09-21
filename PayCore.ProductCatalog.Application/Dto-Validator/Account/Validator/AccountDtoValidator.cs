using FluentValidation;
using PayCore.ProductCatalog.Application.Dto_Validator.Account.Dto;


namespace PayCore.ProductCatalog.Application.Dto_Validator.Account.Validator
{
    public class AccountDtoValidator:AbstractValidator<AccountUpsertDto>
    {
        public AccountDtoValidator()
        {
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(20).NotEmpty().WithMessage("Password length should be between 8-20 character");
            RuleFor(x => x.Name).MinimumLength(3).MaximumLength(50).NotEmpty().WithMessage("Name length should be between 3-50");
            RuleFor(x => x.UserName).MinimumLength(3).MaximumLength(50).NotEmpty().WithMessage("Name length should be between 3-50");
            RuleFor(x => x.Email).EmailAddress().NotEmpty();

        }
    }
}
