using System;
using System.Linq;
using System.Threading.Tasks;
using Books.Models;
using Books.Models.DTOs;
using Books.Services.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Books.App.Controllers.Authentication
{
    // [ApiController]
    // [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserDTO> _userDtoValidator;
        private readonly IValidator<AdminUserDTO> _adminUserDtoValidator;
        private readonly IConfiguration _configuration;
        private readonly IJwtRepository _jwtRepository;

        public AuthController(IUserRepository userRepository, IValidator<UserDTO> userDtoValidator, IValidator<AdminUserDTO> adminUserDtoValidator, IConfiguration configuration, IJwtRepository jwtRepository)
        {
            _userRepository = userRepository;
            _userDtoValidator = userDtoValidator;
            _adminUserDtoValidator = adminUserDtoValidator;
            _configuration = configuration;
            _jwtRepository = jwtRepository;
        }

        // [HttpPost("api/users/register")]
        // public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        // {
        //     // Validar el DTO
        //     var result = _userDtoValidator.Validate(userDTO);
        //     if (!result.IsValid)
        //     {
        //         return BadRequest(result.Errors);
        //     }

        //     // Crear el usuario
        //     try
        //     {
        //         var newUser = await _userRepository.CreateUserAsync(userDTO, userDTO.Password);
        //         return Ok(new { message = "User registered successfully" });
        //     }
        //     catch (InvalidOperationException ex)
        //     {
        //         return Conflict(new { message = ex.Message });
        //     }
        // }

        [HttpPost("api/admin/register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminUserDTO adminUserDTO)
        {
            // Validar el DTO
            var result = _adminUserDtoValidator.Validate(adminUserDTO);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            // Crear el usuario administrador
            try
            {
                var newAdminUser = await _userRepository.CreateAdminUserAsync(adminUserDTO, adminUserDTO.Password);
                return Ok(new { message = "Admin user registered successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("api/auth/login")]
        public async Task<IActionResult> Login([FromBody] AuthResponseDTO authResponseDTO)
        {
            var token = await _userRepository.LoginAsync(authResponseDTO.Email, authResponseDTO.Password);

            /* if (user == null || !BCrypt.Net.BCrypt.Verify(authResponseDTO.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            } */

            // Notificar al usuario sobre el inicio de sesión
            await _userRepository.NotifyLoginAsync(authResponseDTO.Email);

            return Ok(new TokenModelDTO { Token = token });
        }
    }
}
