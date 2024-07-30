using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Books.Infrastructure.Data;
using Books.Models.DTOs.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Books.App.Controllers.Authentication
{
    [ApiController]
    [Route("api/auth/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly BaseContext _context;
        public AuthController(BaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthResponseDto authResponseDto)
        {
            var TokenAuth = _context.UserDatas.FirstOrDefault(auth => auth.Email == authResponseDto.Email && auth.Password == authResponseDto.Password);

            if(TokenAuth is null)
            {
                return NotFound(Utils.Exceptions.StatusError.CreateNotFound());
            }
            else
            {
                var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(@Environment.GetEnvironmentVariable("SecretKey")));

                var SigninCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
                var TokenOptions = new JwtSecurityToken(
                    issuer: @Environment.GetEnvironmentVariable("JwtToken"),
                    audience: @Environment.GetEnvironmentVariable("JwtToken"),
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: SigninCredentials
                );
                var WriteToken = new JwtSecurityTokenHandler().WriteToken(TokenOptions);

                return Ok(new TokenModelDto{ Token = WriteToken });
            }

            return Unauthorized();
        }
    }
}