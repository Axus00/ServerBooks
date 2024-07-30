using Books.Models;

namespace Books.Services.Interface;
public interface IAuthorsRepository
{
  Task<Author> FindByName(string authorName);
}