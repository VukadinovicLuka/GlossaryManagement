using Application.DTOs;
using Application.Services;
using Domain.Repositories;

namespace Infrastucture.Authentication;

public class AuthenticationService : IAuthenticationService
{
    
    private readonly IAuthorRepository _authorRepository;
    private readonly IJwtService _jwtService;

    public AuthenticationService(IAuthorRepository authorRepository, IJwtService jwtService)
    {
        _authorRepository = authorRepository;
        _jwtService = jwtService;
    }
    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var author = await _authorRepository.GetByEmailAsync(loginDto.Email);
        if (author == null || !PasswordHashingService.VerifyPassword(loginDto.Password, author.Password)) return null;

        var token = _jwtService.GenerateToken(author);

        return new AuthResponseDto
        {
            Token = token,
            AuthorId = author.Id,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        };
    }
    
}