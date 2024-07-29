using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Books.Models
{
    public class Autor
    { 
        public enum Status
        {
            Active,
            Removed
        }
        public int Id {get; set;}
        public string? Name {get; set;}

        // [JsonIgnore]
        // public List<Book>? Books { get; }
    }
}