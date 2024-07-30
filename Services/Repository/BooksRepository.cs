using AutoMapper;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.DTOs;
using Books.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Books.Services.Repository
{
  public class BooksRepository : IBooksRepository
  {
    private readonly BaseContext _context;
    private readonly IMapper _mapper;

    public BooksRepository(BaseContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<IEnumerable<BookDTO>> GetAllAsync()
    {
      var data = await _context.Books
        .AsNoTracking()
        .Include(b => b.Authors)
        .ToListAsync();

      return _mapper.Map<IEnumerable<BookDTO>>(data);
    }

    public async Task<BookDTO> GetByIdAsync(int id)
    {
        var book = await _context.Books
            .AsNoTracking()
            .Include(b => b.Authors) // K: Se incluyen los autores
            .FirstOrDefaultAsync(b => b.Id == id);
            
        return _mapper.Map<BookDTO>(book);
    }

    public async Task<BookDTO> CreateAsync(BookDTO bookDTO)
    {
    if(bookDTO is null)
    {
      throw new ArgumentNullException(nameof(bookDTO), "The book cannot be null");
    }
    
      Book book = _mapper.Map<Book>(bookDTO);
      _context.Books.Add(book);
      await _context.SaveChangesAsync();
      return bookDTO;
    }

    public async Task<BookDTO> UpdateAsync(int id, BookDTO book)
    {
      Book existingBook = await _context.Books.FindAsync(id);
      if (existingBook == null) 
        return null;

      existingBook = _mapper.Map<Book>(book);

      _context.Books.Update(existingBook);
      await _context.SaveChangesAsync();
      return book;
    }

    public async Task<int> DeleteAsync(int id)
    {
      throw new NotImplementedException();
      // Book book = await _context.Books.FindAsync(id);
      // if (book == null)
      //   return 0;

      // book.
      // // Se debe realizar una soft deletion

      // _context.Books.Remove(book);
      // return await _context.SaveChangesAsync();
    }
  }
}