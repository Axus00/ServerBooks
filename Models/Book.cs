using System.ComponentModel.DataAnnotations.Schema;
using Books.Models.Enums;

namespace Books.Models
{
    public class Book
    {
        public int Id {get; set;}
        public string? Name {get; set;}
        public int? Quantity {get; set;}
        public StatusEnum? Status {get; set;}
        public int? AuthorId { get; set;}
        
        [ForeignKey("AuthorId")]
        public Author? Authors { get; set; }
    }
}