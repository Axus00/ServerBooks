using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Models;
using Books.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _usersRepository;

        public UsersController(IUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        [Route("api/Users/list")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _usersRepository.GetAllUsersAsync()); // Corregido a _usersRepository
        }

        [HttpGet]
        [Route("api/Users/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id); // Corregido a _usersRepository
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
