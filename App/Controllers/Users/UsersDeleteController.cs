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
        public IActionResult DeleteUser(int id)
        {
            return Ok(_usersRepository.DeleteUserAsync(id));
        }
    }

}