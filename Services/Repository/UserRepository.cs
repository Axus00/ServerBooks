using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Books.Infrastructure.Data;
using Books.Models;
using Books.Models.DTOs;
using Books.Services.Interface;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Books.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailRepository _emailRepository;
        private readonly IJwtRepository _jwtRepository;

        public UserRepository(BaseContext context, IConfiguration configuration, IEmailRepository emailRepository, IJwtRepository jwtRepository)
        {
            _context = context;
            _configuration = configuration;
            _emailRepository = emailRepository;
            _jwtRepository = jwtRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserDatas)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

       public async Task<User> CreateUserAsync(UserDTO userDTO, string password)
        {
            // Verificar si un usuario con el email dado ya existe
            var existingUserData = await _context.UserDatas
                .FirstOrDefaultAsync(ud => ud.Email == userDTO.Email);

            if (existingUserData != null)
                throw new InvalidOperationException("User with the given email already exists.");

            // Encriptar la contraseña
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Crear una nueva instancia de UserData
            var userData = new UserData
            {
                Email = userDTO.Email,
                Password = hashedPassword,
                Phone = userDTO.Phone
            };

            // Crear una nueva instancia de User
            var user = new User
            {
                Names = userDTO.Names,
                Status = "Active", // Estado por defecto o como se necesite
                UserDatas = new List<UserData> { userData } // Asignar la instancia de UserData
            };

            // Agregar user al contexto
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // Guarda para obtener el Id generado

            // Crear UserRole con el Id del nuevo usuario
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = 1 // Asignar rol predeterminado de Customer
            };

            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync(); // Guardar todos los cambios

            // Enviar email de confirmación
            await SendRegistrationEmailAsync(userDTO.Email, password);

            return user;
        }
        public async Task<User> CreateAdminUserAsync(AdminUserDTO adminUserDTO, string password)
        {
            // Verificar si un usuario con el email dado ya existe
            var existingUserData = await _context.UserDatas
                .FirstOrDefaultAsync(ud => ud.Email == adminUserDTO.Email);

            if (existingUserData != null)
                throw new InvalidOperationException("User with the given email already exists.");

            // Encriptar la contraseña
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Crear una nueva instancia de UserData
            var userData = new UserData
            {
                Email = adminUserDTO.Email,
                Password = hashedPassword,
                Phone = adminUserDTO.Phone
            };

            // Crear una nueva instancia de User
            var user = new User
            {
                Names = adminUserDTO.Names,
                Status = "Active", // Estado por defecto o como se necesite
                UserDatas = new List<UserData> { userData } // Asignar la instancia de UserData
            };

            // Agregar user al contexto
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Crear y agregar UserRole
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Type == "Admin");
            if (role != null)
            {
                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };
                _context.UserRoles.Add(userRole);
            }

            await _context.SaveChangesAsync();

            // Enviar email de confirmación
            await SendRegistrationEmailAsync(adminUserDTO.Email, password);

            return user;
        }

       public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserDatas)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Cambiar el estado del usuario a "Removed"
            user.Status = "Removed";

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            // Enviar email de notificación de eliminación
            var userData = user.UserDatas?.FirstOrDefault();
            if (userData != null)
            {
                await SendAccountRemovalEmailAsync(userData.Email);
            }
        }

        private async Task SendAccountRemovalEmailAsync(string email)
        {
            var templateRemovalHtmlRoute = "Utils/Resources/LoginConfirmation.html";

            try
            {
                string templateHtmlContent = File.ReadAllText(templateRemovalHtmlRoute);

                // Reemplazar los placeholders con valores reales
                var messageContent = templateHtmlContent
                    .Replace("{emailUser}", email);

                var subject = "- ServeBook: Account Removal Notification";
                var recipientEmail = email;

                await _emailRepository.SendEmailAsync(recipientEmail, subject, messageContent);
            }
            catch (Exception ex)
            {
                // Manejar excepciones relacionadas con la lectura del archivo o el envío de correo
                throw new InvalidOperationException("Failed to send account removal notification.", ex);
            }
        }


        public async Task<string> LoginAsync(string email, string password)
        {
            var userData = await _context.UserDatas
                // .Include(ud => ud.User)
                .FirstOrDefaultAsync(u => u.Email == email);

            User user = await _context.Users
                .Include(u => u.UserRole)
                .ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(u => u.Id == userData.UserId);

            // UserRole userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            /* if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            } */
            Role userRole = user.UserRole.FirstOrDefault().Role;

            // Generar el token JWT
            var token = _jwtRepository.GenerateToken(userData.Email, user.Names, userRole.Type);

            return token;
        }

        public async Task NotifyLoginAsync(string email)
        {
            var templateLoginHtmlRoute = "Utils/Resources/LoginConfirmation.html";

            try
            {
                string templateHtmlContent = File.ReadAllText(templateLoginHtmlRoute);

                // Reemplazar los placeholders con valores reales
                var messageContent = templateHtmlContent
                    .Replace("{emailUser}", email);

                var subject = "- ServeBook: Login Notification";
                var recipientEmail = email;

                await _emailRepository.SendEmailAsync(recipientEmail, subject, messageContent);
            }
            catch (Exception)
            {
                // Manejar excepciones relacionadas con la lectura del archivo o el envío de correo
                throw;
            }
        }

        public async Task SendRegistrationEmailAsync(string email, string password)
        {
            var templateRegisterHtmlRoute = "Utils/Resources/RegisterConfirmation.html";

            try
            {
                string templateHtmlContent = File.ReadAllText(templateRegisterHtmlRoute);

                // Reemplazar los placeholders con valores reales
                var messageContent = templateHtmlContent
                    .Replace("{emailUser}", email)
                    .Replace("{passwordUser}", password); // Considera las implicaciones de seguridad

                var subject = "- ServeBook: Registration successful";
                var recipientEmail = email;

                await _emailRepository.SendEmailAsync(recipientEmail, subject, messageContent);
            }
            catch (Exception)
            {
                // Manejar excepciones relacionadas con la lectura del archivo o el envío de correo
                throw;
            }
        }
    }
}