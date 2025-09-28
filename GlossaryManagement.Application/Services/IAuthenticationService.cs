using Application.DTOs;

namespace Application.Services;

public interface IAuthenticationService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);

}