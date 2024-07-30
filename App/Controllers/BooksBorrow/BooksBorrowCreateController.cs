using Books.Models;
using Books.Models.DTOs;
using Books.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers.Books
{
  [Route("api/booksborrow")]
  [ApiController]
  // Remember that in works in onion (this is level 1, repository is level 2), so
  // others classes, don't know about this one
  public class BooksBorrowCreateController: ControllerBase
  {
    private readonly IBooksBorrowRepository _booksBorrowRepository;
    public BooksBorrowCreateController(IBooksBorrowRepository booksBorrowRepository)
    {
      _booksBorrowRepository = booksBorrowRepository;
    }

    [HttpPost]
    public async Task<BookBorrow> BorrowBook(BorrowDTO bookBorrow)
    {
      return await _booksBorrowRepository.BorrowBook(bookBorrow.bookId, bookBorrow.userId);
    }
  }
}