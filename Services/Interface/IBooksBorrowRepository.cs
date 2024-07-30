using Books.Models;

namespace Books.Services.Interface;
public interface IBooksBorrowRepository
{
  Task<bool> IsAvalible(int bookId);
  Task<BookBorrow> BorrowBook(int bookId, int userId);
  Task<BookBorrow> ReturnBook(int borrowId);
  Task<BookBorrow> ApproveBookBorrow(int borrowId);
}