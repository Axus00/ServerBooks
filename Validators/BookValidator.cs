using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using FluentValidation;

namespace Books.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            Include(new BookNameRule());
        }

        //Start validations
        public class BookNameRule : AbstractValidator<Book>
        {
            public BookNameRule()
            {
                RuleFor(book => book.Name).NotEmpty().WithMessage("The field is required");
            }
        }
    }
}