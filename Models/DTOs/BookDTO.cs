namespace Books.Models.DTOs
{
  public class BookDTO
  {
    public string? Name {get; set;}
    public int AuthorId { get; }
    public Autor? Authors { get; }
  }
}