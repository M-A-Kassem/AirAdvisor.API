using Graduation_Project.Application.DTOs.Auth;

namespace Graduation_Project.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> CreateUserAsync(CreateUserDto dto);
}

