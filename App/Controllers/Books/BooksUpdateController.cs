using Books.Models.DTOs;
using Books.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers.Books
{
  [Route("api/books")]
  [ApiController]
  public class BooksUpdateController: ControllerBase
  {
    private readonly IBooksRepository _booksRepository;
    public BooksUpdateController(IBooksRepository booksRepository)
    {
      _booksRepository = booksRepository;
    }

    [HttpPut("{Id}")]
    public async Task<BookDTO> Update(int Id, BookDTO book)
    {
      return await _booksRepository.UpdateAsync(Id, book);
    }
  }
}