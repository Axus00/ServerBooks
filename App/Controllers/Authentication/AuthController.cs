using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Books.Infrastructure.Data;
using Books.Services.Interface;
using Books.Services.Repository;
using Books.Models;
using Books.Models.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace Books.App.Controllers.Authentication
{
    public class AuthController : ControllerBase
    {
        private readonly BaseContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserDTO> _userDtoValidator;
        private readonly IValidator<AdminUserDTO> _adminUserDtoValidator;
        private readonly IJwtRepository _jwtRepository;

        public AuthController(BaseContext context, IConfiguration configuration, IUserRepository userRepository, IValidator<UserDTO> userDtoValidator, IValidator<AdminUserDTO> adminUserDtoValidator, IJwtRepository jwtRepository)
        {
            _context = context;
            _configuration = configuration;
            _userRepository = userRepository;
            _userDtoValidator = userDtoValidator;
            _adminUserDtoValidator = adminUserDtoValidator;
            _jwtRepository = jwtRepository;
        }

        


        [HttpPost]
        [Route("api/admin/register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminUserDTO adminUserDTO)
        {
            // Validar el DTO
            var result = _adminUserDtoValidator.Validate(adminUserDTO);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            // Verificar si el usuario ya existe
            var existingUser = await _context.UserDatas
                .FirstOrDefaultAsync(u => u.Email == adminUserDTO.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "The user with this email already exists" });
            }

            // Cifrar la contraseña
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminUserDTO.Password ?? string.Empty);

            // Crear el usuario
            var newUser = new User
            {
                Names = adminUserDTO.Names,
                Status = "Active"
            };

            // Agregar y guardar el usuario
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Crear el UserData
            var newUserData = new UserData
            {
                Email = adminUserDTO.Email,
                Password = hashedPassword,
                Phone = adminUserDTO.Phone,
                UserId = newUser.Id 
            };

            // Obtener el rol de admin
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Type == "Admin");

            if (role != null)
            {  
                // Crear el UserRole
                var newUserRole = new UserRole
                {
                    UserId = newUser.Id,
                    RoleId = role.Id
                };
                _context.UserRoles.Add(newUserRole);
            }

            // Agregar y guardar UserData
            _context.UserDatas.Add(newUserData);
            await _context.SaveChangesAsync();

            // Devolver respuesta exitosa para admin o usuario normal
            if(role.Type == "Admin"){
                return Ok(new { message = "Admin user registered successfully" });
            }

            return Ok(new { message = "User registered successfully" });
        }


        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IActionResult> Login([FromBody] AuthResponseDTO authResponseDTO)
        {
            var user = await _context.UserDatas
                .Include(ud => ud.User) 
                .FirstOrDefaultAsync(u => u.Email == authResponseDTO.Email);

            /* if (user == null || !BCrypt.Net.BCrypt.Verify(authResponseDTO.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            } */

            var token = _jwtRepository.GenerateToken(user.Email, user.User.Names, user.UserRoles?.FirstOrDefault()?.Role?.Type.ToString() ?? "Customer");

            return Ok(new TokenModelDTO { Token = token });
        }


    }
}