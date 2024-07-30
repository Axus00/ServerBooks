using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Books.Models
{
    public class Book
    {
        public int Id {get; set;}
        public string? Name {get; set;}
        public int AuthorId { get; set;}
        public int Quantity { get; set;}
        public string? Status {get; set;}
        
        [ForeignKey("AuthorId")]
        public Autor? Authors { get; set; }

        [JsonIgnore]
        public List<BookBorrow>? BookBorrows { get; set; }

    }
}