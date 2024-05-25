using D2Soft.Application.DTOs;
using FluentValidation;

namespace D2Soft.Application.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().WithMessage("UserName is required.");
            RuleFor(user => user.CNIC).NotEmpty().WithMessage("CNIC is required.")
                                       .Length(13).WithMessage("CNIC must be 13 digits.");
            RuleFor(user => user.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required.")
                                             .Length(11).WithMessage("PhoneNumber must be 11 digits.");
        }
    }
}