using AutoMapper;
using Books.Models;
using Books.Models.DTOs;

namespace Store.ApplicationCore.Mappings
{
  public class StudentProfile : Profile
  {
    public StudentProfile()
    {
      CreateMap<Book, BookDTO>();
      CreateMap<BookDTO, Book>();
    }
  }
}