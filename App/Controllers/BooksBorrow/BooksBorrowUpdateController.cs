using Books.Models;
using Books.Models.DTOs;
using Books.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers.Books
{
  // [Route("api/booksborrow")]
  [ApiController]
  // Remember that in works in onion (this is level 1, repository is level 2), so
  // others classes, don't know about this one
  public class BooksBorrowUpdateController: ControllerBase
  {
    private readonly IBooksBorrowRepository _booksBorrowRepository;
    public BooksBorrowUpdateController(IBooksBorrowRepository booksBorrowRepository)
    {
      _booksBorrowRepository = booksBorrowRepository;
    }

    
    
    [HttpPut]
    [Route("api/booksborrow/approve/{Id}")]
    public async Task<BookBorrow> ApproveBook(int Id)
    {
      return await _booksBorrowRepository.ApproveBookBorrow(Id);
    }

    [HttpPut]
    [Route("api/booksborrow/return/{Id}")]
    public async Task<BookBorrow> ReturnBook(int Id)
    {
      return await _booksBorrowRepository.ReturnBook(Id);
    }
  }
}