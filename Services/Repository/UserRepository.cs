using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.DTOs;
using Books.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Books.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;
        string Error { get; set; } = string.Empty;

        public UserRepository(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var user = await _context.Users
                .Include(c => c.UserRole)
                /* .Include(c => c.UserData) */
                .ToListAsync();

            if(user is null || !user.Any())
            {
                Error += "Users no found";
            }

            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var userById = await _context.Users
                .Include(m => m.UserRole)
                /* .Include(c => c.UserData) */
                .FirstOrDefaultAsync(c => c.Id == id);
            if(userById == null)
            {
                Error += "user id not found";
            }
            return userById;
        }

        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            if(userDto is null)
            {
                throw new ArgumentNullException(nameof(userDto), "The user cannot be null");
            }


            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            _mapper.Map(userDTO, user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            if (user.Status != "Removed")
            {
                user.Status = "Removed"; 
                await _context.SaveChangesAsync();
            }

            return user;
        }

        //Register 
        public async Task<User> CreateUserAsync(UserDTO userDTO)
        {
            // Se encripta la contraseña
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            userDTO.Password = hashedPassword;
            
            var user = _mapper.Map<User>(userDTO);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
