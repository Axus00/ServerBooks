using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Books.Models
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
        [JsonIgnore]
        public User? Users { get; }
        public int BookId { get; }
        [JsonIgnore]
        public Book? Books { get; }

    }
}