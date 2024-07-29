using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Books.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        List<User>? Users {get; set; }
    }
}