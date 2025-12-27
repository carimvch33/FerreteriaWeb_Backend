using FerreteríaWeb_Backend.Models.DTOs.Auth;
using FerreteríaWeb_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FerreteríaWeb_Backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var response = _authService.Login(dto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Por favor intente más tarde.");
            }
        }
    }
}
