using eCommerce.Application.DTOs.Authentication;
using FluentValidation;

namespace eCommerce.Application.Validations.Authentication
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.fullName).NotEmpty().WithMessage("Full Name is required")
                .MinimumLength(3).WithMessage("Name must be at least three characters long");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email Format");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .MaximumLength(32).WithMessage("Password must be more 32 characters long")
                .Matches(@"[A-Z]").WithMessage("Password Must Contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase character")
                .Matches(@"[\d]").WithMessage("Password must have at least one digit character")
                .Matches(@"[^\w]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Password and Confirm Password do not match");

        }
    }
}
