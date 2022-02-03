using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pre_Aceleracion_Julieta_Sosa.Interfaces;
using Pre_Aceleracion_Julieta_Sosa.Models;
using Pre_Aceleracion_Julieta_Sosa.ViewModels.Auth;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Pre_Aceleracion_Julieta_Sosa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TokenController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Authentication(Login login)
        {
            //si es un user valido
            var user = IsValidUser(login);

            if (user != null)
            {
                var token = GenerateToken(user.Name, user.Email);
                return Ok(new { token });
            }
            return BadRequest("El usuario o la contraseña son incorrectas");
        }

        private User IsValidUser(Login login)
        {
            var users = _unitOfWork.UserRepository.GetAll();
            var user = users.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();

            return user;
        }

        private string GenerateToken(string name, string email)
        {
            //Headereader
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Julieta"),
                new Claim(ClaimTypes.Email, "julisoad15@gmail.com"),
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
