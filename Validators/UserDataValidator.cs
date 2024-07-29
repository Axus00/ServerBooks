using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using FluentValidation;

namespace Books.Validators
{
    public class UserDataValidator : AbstractValidator<UserData>
    {
        public UserDataValidator()
        {
            Include(new UserEmailRule());
            Include(new UserPhoneRule());
        }

        //Start Validations
        public class UserEmailRule : AbstractValidator<UserData>
        {
            public UserEmailRule()
            {
                RuleFor(user => user.Email).NotNull().WithMessage("The Email must have the characteristics as email addres");

                RuleFor(user => user.Email).EmailAddress();
            }
        }

        public class UserPhoneRule : AbstractValidator<UserData>
        {
            public UserPhoneRule()
            {
                RuleFor(user => user.Phone).Cascade(CascadeMode.Stop).NotNull().WithMessage("The field is required").Matches(@"^\d{10,15}$").WithMessage("The number must be between 10 and 15 digits long and only numbers");
            }
        }
    }
}