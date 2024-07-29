using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models.DTOs;

public interface IBooksRepository
{
  Task<IEnumerable<BookDTO>> GetAllAsync();
  Task<BookDTO> GetByIdAsync(int id);
  Task<BookDTO> CreateAsync(BookDTO book);
  Task<BookDTO> UpdateAsync(int id, BookDTO book);
  Task<int> DeleteAsync(int id);
}