using AutoMapper;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Repository
{
  public class AuthorsRepository : IAuthorsRepository
  {
    private readonly BaseContext _context;
    private readonly IMapper _mapper;

    public AuthorsRepository(BaseContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    public async Task<Author> FindByName(string authorName)
    {
      Author author = await _context.Authors.FirstOrDefaultAsync((a) => a.Name == authorName);
      return author;
    }

    // private string CleanName(string name)
    // {
    //   string putInLowerCase = name.ToLower();
    //   string removeWhiteSpaces = putInLowerCase
    //     .Trim()
    //     .Replace(" ", "");
    //   return removeWhiteSpaces;
    // }
  }
}