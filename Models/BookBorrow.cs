using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Models.Enums;

namespace Books.Models
{
    public class BookBorrow
    {
        public int Id {get; set;}
        public DateTime? StartDate  {get; set;}
        public DateTime? EndDate   {get; set;}
        public BorrowStatusEnum BorrowStatus {get; set;}
        public int UserId { get; set; }
        public User? Users { get; }
        public int BookId { get; set; }
        public Book? Books { get; }

    }
}