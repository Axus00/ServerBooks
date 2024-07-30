using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.DTOs;
using Books.Services.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.App.Controllers.Users
{
    [ApiController]
    [Route("api/users/[action]")]
    public class UserCreateController : ControllerBase
    {
        private readonly BaseContext _context;
        private readonly IUserRepository _userRepository;

        private readonly IValidator<UserDTO> _userDtoValidator;
        public UserCreateController(BaseContext context, IUserRepository userRepository, IValidator<UserDTO> userDtoValidator)
        {
            _context = context;
            _userRepository = userRepository;
            _userDtoValidator = userDtoValidator;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            // Validar el DTO
            var result = _userDtoValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            // Verificar si el usuario ya existe
            var existingUser = await _context.UserDatas
                .FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "The user with this email already exists" });
            }

            // Cifrar la contraseña
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            

            // Crear el usuario
            var newUser = new User
            {
                Names = userDTO.Names,
                Status = "Active"
            };

            // Agregar y guardar el usuario
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Crear el UserData
            var newUserData = new UserData
            {
                Email = userDTO.Email,
                Password = hashedPassword,
                Phone = userDTO.Phone,
                UserId = newUser.Id 
            };

            var newUserRole = new UserRole
            {
                UserId = newUser.Id,
                RoleId = 1 // Asignar rol predeterminado de Customer
            };

            // Agregar y guardar UserData y UserRole
            _context.UserDatas.Add(newUserData);
            _context.UserRoles.Add(newUserRole);
            await _context.SaveChangesAsync();

            // Devolver respuesta exitosa
            return Ok(new { message = "User registered successfully" });
        }
    }
}