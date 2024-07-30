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

            public UsersDeleteController(IUserRepository usersRepository)
            {
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
                catch (InvalidOperationException)
                {
                    return NotFound(Utils.Exceptions.StatusError.CreateNotFound());
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