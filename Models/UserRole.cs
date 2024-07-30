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

        [JsonIgnore]
        public List<User>? Users { get; set; }

        [JsonIgnore]
        List<Role>? Roles { get; set; }
    }
}
