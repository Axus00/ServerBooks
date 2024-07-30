using AutoMapper;
using Books.Models;
using Books.Models.DTOs;

namespace Books.Models.Mappers
{
  public class BooksProfile : Profile
  {
    public BooksProfile()
    {
      CreateMap<Book, BookDTO>()
        .ForMember(dest =>
          dest.Author,
          opt => opt.MapFrom(src => src.Authors.Name)
        );
      CreateMap<BookDTO, Book>();
      // CreateMap<Book, AuthorDTO>();
    }
  }
}