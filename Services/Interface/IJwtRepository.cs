using Books.Models.DTOs;

namespace Books.Services.Interface
{
    public interface IJwtRepository
    {
        string GenerateToken(string email, string name, string role);
    }
}
