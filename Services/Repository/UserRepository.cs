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

        public UserRepository(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(c => c.UserRole)
                /* .Include(c => c.UserData) */
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(m => m.UserRole)
                /* .Include(c => c.UserData) */
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User> CreateUserAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, UserDTO userDTO)
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

    }
}
