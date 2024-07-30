using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Books.Services.Interface;
using Books.Services.Repository;
using Books.Models.DTOs;

namespace Books.App.Controllers
{
    public class UsersDeleteController : ControllerBase
    {
        private readonly IUserRepository _usersRepository;

        public UsersDeleteController(IUserRepository usersRepository){
            _usersRepository = usersRepository;
        } 

       [HttpDelete]
        [Route("api/users/{id}/delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _usersRepository.DeleteUserAsync(id);
            if (user == null)
            {
                return NotFound(); // Devuelve NotFound si el usuario no existe
            }

            if (user.Status == "Removed")
            {
                return Ok(new { user, message = "User was already in 'Removed' status." });
            }

            return Ok(new { user, message = "User status updated to 'Removed'." });
        }
    }

}