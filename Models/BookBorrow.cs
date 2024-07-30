using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Models.Enums;

namespace Books.Models
{
    public class BookBorrow
    {
        public int Id {get; set;}
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate {get; set;}
        public BorrowStatusEnum BorrowStatus {get; set;}
        public int UserId { get; set; }
        public int BookId { get; set; }

        [ForeignKey("UserId")]
        public User? Users { get; set; }

        [ForeignKey("BookId")]
        public Book? Books { get; set; }

    }
}