using AutoMapper;
using Books.Models;
using Books.Models.DTOs;

namespace Books.Models.Mappers
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