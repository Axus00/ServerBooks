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
        [Route("api/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if(!ModelState.IsValid)
            {
                Utils.Exceptions.StatusError.CreateBadRequest();
            }

            try
            {
                return Ok(await _usersRepository.GetAllUsersAsync()); // Corregido a _usersRepository
            }
            catch (Exception)
            {
                var problemDetails = Utils.Exceptions.StatusError.CreateServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
                throw;
            }
            
        }

        [HttpGet]
        [Route("api/users/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id); // Corregido a _usersRepository
            
            try
            {
                if (user == null)
                {
                    return NotFound(Utils.Exceptions.StatusError.CreateNotFound());
                }
                return Ok(user);
            }
            catch (Exception)
            {
                var problemDetails = Utils.Exceptions.StatusError.CreateServerError();
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
                throw;
            }
            
        }
    }
}
