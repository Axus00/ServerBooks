using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Books.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; } // Relación con la clase User
    }
}
