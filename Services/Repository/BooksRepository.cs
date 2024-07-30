using AutoMapper;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.DTOs;
using Books.Models.Enums;
using Books.Services.Interface;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Books.Services.Repository;
public class BooksRepository : IBooksRepository
{
  private readonly BaseContext _context;
  private readonly IMapper _mapper;
  private readonly IAuthorsRepository _authorsRepository;

  public BooksRepository(BaseContext context, IMapper mapper, IAuthorsRepository authorsRepository)
  {
    _context = context;
    _mapper = mapper;
    _authorsRepository = authorsRepository;
  }

  public async Task<IEnumerable<BookDTO>> GetAllAsync()
  {
    var data = await _context.Books
      .AsNoTracking()
      .Where(b => b.Status == StatusEnum.Active)
      .Include(b => b.Authors)
      .ToListAsync();

    return _mapper.Map<IEnumerable<BookDTO>>(data);
  }

  public async Task<BookDTO> GetByIdAsync(int id)
  {
    Book book = await _context.Books
      .Include(b => b.Authors)
      .FirstOrDefaultAsync(b => b.Id == id);
    return _mapper.Map<BookDTO>(book);
  }

  public async Task<BookDTO> CreateAsync(BookDTO bookDTO)
  {
    Books.Models.Author existAuthor = await GetAuthor(bookDTO.Author);

    // Tirar excepcion si el autor no existe
    if (existAuthor == null) 
      return null;

    if(bookDTO is null)
    {
      throw new ArgumentNullException(nameof(bookDTO), "The book cannot be null");
    }
    
    Book book = _mapper.Map<Book>(bookDTO);
    book.AuthorId = existAuthor.Id;
    book.Status = StatusEnum.Active;

    _context.Books.Add(book);
    await _context.SaveChangesAsync();
    return bookDTO;
  }

  public async Task<BookDTO> UpdateAsync(int id, BookDTO book)
  {
    Book existingBook = await _context.Books.FindAsync(id);
    if (existingBook == null) 
      return null;

    Books.Models.Author existAuthor = await GetAuthor(book.Author);
    if (existAuthor == null) 
      return null;

    existingBook = _mapper.Map<BookDTO, Book>(book, existingBook);
    existingBook.AuthorId = existAuthor.Id;
    
    _context.Books.Update(existingBook);
    await _context.SaveChangesAsync();
    return book;
  }

  public async Task<int> DeleteAsync(int id)
  {
    // throw new NotImplementedException();
    Book book = await _context.Books.FindAsync(id);
    if (book == null)
      return 0;

    book.Status = StatusEnum.Removed;

    _context.Books.Update(book);
    return await _context.SaveChangesAsync();
  }

  private async Task<Books.Models.Author> GetAuthor(string authorName)
  {
    return await _authorsRepository.FindByName(authorName); 
  } 

}