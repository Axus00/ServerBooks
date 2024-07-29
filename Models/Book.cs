namespace Books.Models
{
    public class Book
    {
        public int Id {get; set;}
        public string? Name {get; set;}
        public int AuthorId { get; }
        public Autor? Authors { get; }

    }
}