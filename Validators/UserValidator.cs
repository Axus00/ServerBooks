using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using FluentValidation;

namespace Books.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            Include(new UserNameRule());
            
        }

        //Start Validations
        public class UserNameRule : AbstractValidator<User>
        {
            private readonly List<string> OfensiveNames = new List<string>
            {
                "XXX", "Asshole", "Bitch", "Bastard", "Damn", "Dick", "Dumbass", "Fuck", "Idiot", "Jerk", "Moron", "Prick", "Shit", "Stupid", "Wanker"
            };
            public UserNameRule()
            {

                RuleFor(user => user.Names).NotNull().WithMessage("The field Names is required");

                RuleFor(user => user.Names).Must(Names => !OfensiveNames.Contains(Names)).WithMessage("You can't put Last Name ofensive");
            }
        }
    }
}