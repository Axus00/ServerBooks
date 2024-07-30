using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.DTOs;
using Books.Models.Enums;
using Books.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Books.Services.Repository
{
  public class BooksBorrowRepository : IBooksBorrowRepository
  {
    private readonly BaseContext _context;
    private readonly IMapper _mapper;
    private readonly IBooksRepository _booksRepository;
    private readonly IEmailRepository _emailRepository;

    public BooksBorrowRepository(BaseContext context,
      IMapper mapper,
      IBooksRepository bookRepository,
      IEmailRepository emailRepository
    )
    {
      _context = context;
      _mapper = mapper;
      _booksRepository = bookRepository;
      _emailRepository = emailRepository;
    }
    
    /*
      ## Most to create a manager
      BooksBorrow mustn't have to know about book information, 
      only have to give a number of book borrow and outside of this class make a comparation
      to validate if avalible 

      #### Example
      BookManager.IsAvalible(bookId)
      {
        b1 = BookBorrowRepository.NumberOfBorrow(bookId)
        b2 = BookRepository.Find(bookId)

        if(b1 => b2.Quantity)
          return false
      }
    */
    public async Task<bool> IsAvalible(int bookId)
    {
      BookDTO getBookInfo = await _booksRepository.GetByIdAsync(bookId);
      int numberOfBorrowBooks = await GetNumberBorrowBooks(bookId);

      if(numberOfBorrowBooks > getBookInfo.Quantity)
      {
        TooManyBooksBorrow(numberOfBorrowBooks, getBookInfo);
        return false;
      }

      if(numberOfBorrowBooks == getBookInfo.Quantity)
        return false;

      return true;
    }

    public async Task<BookBorrow> BorrowBook(int bookId, int userId)
    {
      if(!await ExistBook(bookId) || !await ExistUser(userId))
      {
        Utils.Exceptions.StatusError.CreateNotFound();
        return null;
      }

      if(! await IsAvalible(bookId))
      {
        return null;
      }

      BookBorrow bookBorrow = new()
      {
        BookId = bookId,
        UserId = userId,
        StartDate = DateTime.MinValue,
        EndDate = DateTime.MinValue,
        BorrowStatus = BorrowStatusEnum.Pending
      };

      _context.BookBorrows.Add(bookBorrow);
      await _context.SaveChangesAsync();
      return bookBorrow;
    }

    public async Task<BookBorrow> ReturnBook(int borrowId)
    {
      BookBorrow bookBorrow = await _context.BookBorrows.FindAsync(borrowId);

      if(bookBorrow == null)
        return null;

      bookBorrow.BorrowStatus = BorrowStatusEnum.Returned;

      _context.BookBorrows.Update(bookBorrow);
      await _context.SaveChangesAsync();
      return bookBorrow;
    }

    public async Task<BookBorrow> ApproveBookBorrow(int borrowId)
    {
      BookBorrow bookBorrow = await _context.BookBorrows.FindAsync(borrowId);

      if(bookBorrow == null)
        return null;

      bookBorrow.BorrowStatus = BorrowStatusEnum.Approved;
      bookBorrow.StartDate = BaseDate();
      bookBorrow.EndDate = OnlyBorrowForTenDays();

      _context.BookBorrows.Update(bookBorrow);
      await _context.SaveChangesAsync();
      return bookBorrow;
    }

    private async Task<int> GetNumberBorrowBooks(int bookId)
    {
      return await _context.BookBorrows
      .AsNoTracking()
      .Where(b => b.BorrowStatus == BorrowStatusEnum.Approved)
      .CountAsync();
    }

    private void TooManyBooksBorrow(int numberOfBorrows, BookDTO bookInfo)
    {
      _emailRepository.SendEmailAsync("example@gmail.com", "Invalid number of borrows", 
          $"It looks like there are too many books request of book {JsonSerializer.Serialize(bookInfo)} with borrow copies #: {numberOfBorrows}"
        );
    }

    private async Task<bool> ExistBook(int bookId)
    {
      BookDTO book = await _booksRepository.GetByIdAsync(bookId);
      return book is not null;
    }

    private async Task<bool> ExistUser(int bookId)
    {
      BookDTO book = await _booksRepository.GetByIdAsync(bookId);
      return book is not null;
    }

    private DateTime BaseDate()
    {
      return DateTime.Today;
    }

    // Base of Business rule of only 10 days
    private DateTime OnlyBorrowForTenDays()
    {
      DateTime initialDate = BaseDate();
      return initialDate.Add(new TimeSpan(10, 0, 0, 0));
    }
  }
}