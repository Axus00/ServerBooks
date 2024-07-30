using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Models.DTOs;
using FluentValidation;

namespace Books.Validators
{
    public class AutorDTOValidator : AbstractValidator<AuthorDTO>
    {
        public AutorDTOValidator()
        {
            Include(new AutorNameRule());
        }

        //Start Validations
        public class AutorNameRule : AbstractValidator<AuthorDTO>
        {
            public AutorNameRule()
            {
                RuleFor(autor => autor.Name).NotNull().WithMessage("The field is required");
            }
        } 
    }
}