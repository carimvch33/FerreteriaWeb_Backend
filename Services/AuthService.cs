using FerreteríaWeb_Backend.DAOs.Interfaces;
using FerreteríaWeb_Backend.Models.DTOs.Auth;
using FerreteríaWeb_Backend.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FerreteríaWeb_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeDao _employeeDao;
        private readonly IConfiguration _configuration;

        public AuthService(IEmployeeDao employeeDao, IConfiguration configuration)
        {
            _employeeDao = employeeDao;
            _configuration = configuration;
        }

        public LoginResponseDto Login(LoginRequestDto dto)
        {
            var employee = _employeeDao.GetByEmail(dto.Email);

            if (employee == null || !employee.IsActive)
                throw new InvalidOperationException("Credenciales inválidas.");

            if (employee.PasswordHash != HashPassword(dto.Password))
                throw new InvalidOperationException("Credenciales inválidas.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Role, employee.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["Jwt:ExpiresInMinutes"]!)
                ),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Role = employee.Role.ToString(),
                Token = tokenHandler.WriteToken(token)
            };
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(
                sha.ComputeHash(Encoding.UTF8.GetBytes(password))
            );
        }
    }
}
