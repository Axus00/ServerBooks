using Books.Models.DTOs;
using Books.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Books.Services.Interface;
using Books.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers
{
  [Route("api/books")]
  [ApiController]
  // Remember that in works in onion (this is level 1, repository is level 2), so
  // others classes, don't know about this one
  public class BooksController: ControllerBase
  {
    private readonly IBooksRepository _booksRepository;
    public BooksController(IBooksRepository booksRepository)
    {
      _booksRepository = booksRepository;
    }

    [HttpGet]
    [Authorize] 
    /*
      ## BUG: 91
    */
    public async Task<IEnumerable<BookDTO>> GetAll()
    {
      return await _booksRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<BookDTO> GetById(int Id)
    {
      return await _booksRepository.GetByIdAsync(Id);
    }

  }
}