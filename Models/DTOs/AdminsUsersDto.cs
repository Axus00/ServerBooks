using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models.DTOs
{
    public class AdminUserDTO
    {
        public string ? Names { get; set;}
        public string ? Email { get; set;}
        public string ? Password { get; set;}
        public string ? Phone { get; set;}
        public string ? Status { get; set;}
        public string ? Role { get; set;}
    }
}