using AutoMapper;
using Books.Models;
using Books.Models.DTOs;

namespace Store.ApplicationCore.Mappings
{
  public class BooksProfile : Profile
  {
    public BooksProfile()
    {
      CreateMap<Book, BookDTO>();
      CreateMap<BookDTO, Book>();
    }
  }
}