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
        [Route("api/users/create")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            // Validar el DTO
            var result = _userDtoValidator.Validate(userDTO);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            try
            {
                // Generar una contraseña temporal para el registro (o usa la contraseña que se envía)
                var password = "UserGeneratedPassword"; // Cambia esto según tu lógica para generar o recibir la contraseña

                // Crear el usuario utilizando el repositorio
                var newUser = await _userRepository.CreateUserAsync(userDTO, password);

                // Devolver respuesta exitosa
                return Ok(new { message = "User registered successfully" });
            }
            catch (InvalidOperationException ex)
            {
                // Manejar caso en el que el usuario ya existe
                return Conflict(new { message = ex.Message });
            }
        }

    }
}