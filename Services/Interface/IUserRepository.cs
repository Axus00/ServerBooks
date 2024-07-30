using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using Books.Models.DTOs;

namespace Books.Services.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserDTO userDTO, string password);
        Task<User> UpdateUserAsync(int id, UserDTO userDTO);
        Task<User> DeleteUserAsync(int id);
    }
}
