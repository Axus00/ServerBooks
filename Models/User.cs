using System.Text.Json.Serialization;

namespace Books.Models
{
   public class User
    {
        public int Id { get; set; }
        public string? Names { get; set; }
        public string? Status { get; set; }

        [JsonIgnore]
        public List<UserData>? UserDatas { get; set; }

        [JsonIgnore]
        public List<BookBorrow>? BookBorrows { get; set; }

        [JsonIgnore]
        public List<UserRole>? UserRoles { get; set; }
    }

}

