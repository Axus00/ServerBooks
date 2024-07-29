using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Book
    {
        public int Id {get; set;}
        public string? Name {get; set;}
        public int AuthorId { get; }
        public Autor? Authors { get; }

    }
}