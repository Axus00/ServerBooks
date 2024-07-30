using Books.Models.DTOs;
using Books.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers.Books
{
  [Route("api/books")]
  [ApiController]
  public class BooksDeleteController: ControllerBase
  {
    private readonly IBooksRepository _booksRepository;
    public BooksDeleteController(IBooksRepository booksRepository)
    {
      _booksRepository = booksRepository;
    }

    [HttpDelete("{Id}")]
    public async Task<int> Delete(int Id)
    {
      return await _booksRepository.DeleteAsync(Id);
    }
  }
}