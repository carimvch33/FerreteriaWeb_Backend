using FerreteríaWeb_Backend.Models.DTOs.Auth;

public interface IAuthService
{
    LoginResponseDto Login(LoginRequestDto dto);
}
