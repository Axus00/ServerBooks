using Books.Models.DTOs;
using Books.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers.Books
{
  [Route("api/booksborrow")]
  [ApiController]
  // Remember that in works in onion (this is level 1, repository is level 2), so
  // others classes, don't know about this one
  public class BooksBorrowController: ControllerBase
  {
    private readonly IBooksBorrowRepository _booksBorrowRepository;
    public BooksBorrowController(IBooksBorrowRepository booksBorrowRepository)
    {
      _booksBorrowRepository = booksBorrowRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<BookDTO>> GetAll()
    {
      // return await _booksBorrowRepository.GetAllAsync();
      throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<BookDTO> GetById(int Id)
    {
      throw new NotImplementedException();
      // return await _booksBorrowRepository.GetByIdAsync(Id);
    }

  }
}