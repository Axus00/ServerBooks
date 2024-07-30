using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Books.Models
{
    public class Book
    {
        public enum Status
        {
            Active,
            Removed
        }
        public int Id {get; set;}
        public string? Name {get; set;}
        public int? AuthorId { get; set;}
        [JsonIgnore]
        public Autor? Authors { get; }

    }
}