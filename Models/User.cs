using System.Text.Json;
using System.Text.Json.Serialization;

namespace Books.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Names { get; set; }
        [JsonIgnore]
        public UserData? UserData { get; set; }
        public List<UserRole>? UserRoles { get; set; } 
        // Lista para múltiples roles
        public string? Status { get; set; }
        public ICollection<BookBorrow>? BookBorrows { get; set; }
    }
}

