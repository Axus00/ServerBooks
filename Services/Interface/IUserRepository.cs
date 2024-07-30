using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using Books.Models.Dtos;

namespace Books.Services.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserDto userDto);
        Task<User> UpdateUserAsync(int id, UserDto userDto);
        Task<User> DeleteUserAsync(int id);
    }
}
