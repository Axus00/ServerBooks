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
        public DateTime StartDate  {get; set;}
        public DateTime EndDate   {get; set;}
        public BorrowStatusEnum Status {get; set;}
        public int UserId { get; }
        public User? Users { get; }
        public int BookId { get; }
        public Book? Books { get; }

    }
}