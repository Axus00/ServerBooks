using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;

namespace ServerBooks.Models
{
    public class BookBorrow
    {
        public int Id {get; set;}
        public enum BorrowStatus  {
            Pending,
            Approved,
            Returned
        }
        public DateTime StartDate  {get; set;}
        public DateTime EndDate   {get; set;}

        public int UserId { get; }
        public User? Users { get; }
        public int BookId { get; }
        public Book? Books { get; }

    }
}