using Books.Models.DTOs;
using Books.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers.Books
{
  [Route("api/books")]
  [ApiController]
  public class BooksCreateController: ControllerBase
  {
    private readonly IBooksRepository _booksRepository;
    public BooksCreateController(IBooksRepository booksRepository)
    {
      _booksRepository = booksRepository;
    }

    [HttpPost]
    public async Task<BookDTO> Create(BookDTO book)
    {
      return await _booksRepository.CreateAsync(book);
    }
  }
}