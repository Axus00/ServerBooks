using Books.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers.Books
{
  [Route("api/books")]
  [ApiController]
  // Remember that in works in onion (this is level 1, repository is level 2), so
  // others classes, don't know about this one
  public class BooksController: ControllerBase
  {
    private readonly IBooksRepository _booksRepository;
    public BooksController(IBooksRepository studentsService)
    {
      _booksRepository = studentsService;
    }

    [HttpGet]
    public async Task<IEnumerable<BookDTO>> GetAll()
    {
      return await _booksRepository.GetAllAsync();
    }

    [HttpGet("{Id}")]
    public async Task<BookDTO> GetById(int Id)
    {
      return await _booksRepository.GetByIdAsync(Id);
    }

  }
}