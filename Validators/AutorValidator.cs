using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using FluentValidation;

namespace Books.Validators
{
    public class AutorValidator : AbstractValidator<Autor>
    {
        public AutorValidator()
        {
            Include(new AutorNameRule());
        }

        //Start Validations
        public class AutorNameRule : AbstractValidator<Autor>
        {
            public AutorNameRule()
            {
                RuleFor(autor => autor.Name).NotNull().WithMessage("The field is required");
            }
        } 
    }
}