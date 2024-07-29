using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.Dtos;
using Books.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
                .Include(c => c.UserRoles)
                .Include(c => c.UserData)
                .ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(m => m.UserRoles)
                .Include(c => c.UserData)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            _mapper.Map(userDto, user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Status = "Removed"; // Asegúrate de que la propiedad Status exista en User
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
