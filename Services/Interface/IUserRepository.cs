using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;
using Books.Models.DTOs;

namespace Books.Services.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserDTO userDTO, string password);
        Task<User> CreateAdminUserAsync(AdminUserDTO adminUserDTO, string password);
        Task DeleteUserAsync(int userId);
        Task<string> LoginAsync(string email, string password);
        Task NotifyLoginAsync(string email);
        Task SendRegistrationEmailAsync(string email, string password);
    }
}
