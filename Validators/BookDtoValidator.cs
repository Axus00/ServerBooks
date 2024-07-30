using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Models.DTOs;
using FluentValidation;

namespace Books.Validators
{
    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator()
        {
            Include(new BookNameRule());
        }

        //Start validations
        public class BookNameRule : AbstractValidator<BookDTO>
        {
            public BookNameRule()
            {
                RuleFor(book => book.Name).NotEmpty().WithMessage("The field is required");
            }
        }
    }
}