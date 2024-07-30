using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Books.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User? User { get; set; } // Relación con la clase User
        public Role? Role { get; set; } // Relación con la clase Role

        // [JsonIgnore]
        // public List<User>? Users { get; set; }

        // [JsonIgnore]
        // public List<Role>? Roles { get; set; }
    }
}
