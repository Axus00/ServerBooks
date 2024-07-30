using System.Text.Json.Serialization;
using Books.Models.Enums;

namespace Books.Models
{
    public class Author
    { 
        public int Id {get; set;}
        public string? Name {get; set;}
        public StatusEnum? Status {get; set;}

        [JsonIgnore]
        public List<Book>? Books { get; }
    }
}