using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Models.Dtos;
using FluentValidation;

namespace Books.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            Include(new UserNameRule());
            Include(new UserEmailRule());
            Include(new UserPhoneRule());
            
        }

        //Start Validations
        public class UserNameRule : AbstractValidator<UserDto>
        {
            private readonly List<string> OfensiveNames = new List<string>
            {
                "XXX", "Asshole", "Bitch", "Bastard", "Damn", "Dick", "Dumbass", "Fuck", "Idiot", "Jerk", "Moron", "Prick", "Shit", "Stupid", "Wanker"
            };
            public UserNameRule()
            {

                RuleFor(user => user.Name).NotNull().WithMessage("The field Names is required");

                RuleFor(user => user.Name).Must(Names => !OfensiveNames.Contains(Names)).WithMessage("You can't put Last Name ofensive");
            }
        }

        public class UserEmailRule : AbstractValidator<UserDto>
        {
            public UserEmailRule()
            {
                RuleFor(user => user.Email).NotNull().WithMessage("The Email must have the characteristics as email addres");

                RuleFor(user => user.Email).EmailAddress();
            }
        }

        public class UserPhoneRule : AbstractValidator<UserDto>
        {
            public UserPhoneRule()
            {
                RuleFor(user => user.Phone).Cascade(CascadeMode.Stop).NotNull().WithMessage("The field is required").Matches(@"^\d{10,15}$").WithMessage("The number must be between 10 and 15 digits long and only numbers");
            }
        }
    }
}