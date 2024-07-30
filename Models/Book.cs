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
        public Autor? Authors { get; }

    }
}