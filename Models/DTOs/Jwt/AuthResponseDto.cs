using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Models.DTOs
{
    public class AuthResponseDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}