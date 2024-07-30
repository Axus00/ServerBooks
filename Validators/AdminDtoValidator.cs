using FluentValidation;
using Books.Models;
using Books.Models.DTOs;

namespace Books.Validators
{
    public class AdminUserDTOValidator : AbstractValidator<AdminUserDTO>
    {
        public AdminUserDTOValidator()
        {
            RuleFor(admin => admin.Names)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(admin => admin.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(admin => admin.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits long and contain only numbers");

            RuleFor(admin => admin.Status)
                .NotEmpty().WithMessage("Status is required");

            RuleFor(admin => admin.Role)
                .NotEmpty().WithMessage("Role is required")
                .Must(role => role == "Customer" || role == "Admin")
                .WithMessage("Invalid role specified");
        }
    }
}
