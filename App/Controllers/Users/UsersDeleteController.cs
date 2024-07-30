using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Books.Services.Interface;
using Books.Services.Repository;
using Books.Models.Dtos;

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
            var result = await _usersRepository.DeleteUserAsync(id);
            if (result == null)
            {
                return NotFound(); // Devuelve NotFound si el usuario no existe
            }
            return Ok(result); // Devuelve el usuario actualizado si la eliminación fue exitosa
        }

    }

}