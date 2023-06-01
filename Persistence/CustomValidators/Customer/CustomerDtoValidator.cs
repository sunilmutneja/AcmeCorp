using Domain.Entities.Customer;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Persistence.CustomValidators.Customer
{
    public class CustomerDtoValidator : AbstractValidator<createCustomerDTO>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name cannot be null or an empty string"); ;
            RuleFor(x => x.Mobile).Length(0, 10).WithMessage("Mobile cannot be null or an empty string"); ;
            RuleFor(x => x.Email).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().EmailAddress().WithMessage("Email cannot be null or an empty string");
        }
    }
}
