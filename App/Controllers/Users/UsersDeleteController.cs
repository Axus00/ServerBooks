using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Books.Services.Interface;
using Books.Services.Repository;
using Books.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Books.App.Controllers
{
    public class UsersDeleteController : ControllerBase
    {
        private readonly IUserRepository _usersRepository;
        private object _userRepository;

        public UsersDeleteController(IUserRepository usersRepository){
            _usersRepository = usersRepository;
        } 

            [HttpDelete("api/users/{id}")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> DeleteUser(int id)
            {
                try
                {
                    await _usersRepository.DeleteUserAsync(id);
                    return Ok(new { message = "User marked as removed successfully." });
                }
                catch (InvalidOperationException ex)
                {
                    return NotFound(new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "An error occurred while removing the user.", details = ex.Message });
                }
            }

    }

}