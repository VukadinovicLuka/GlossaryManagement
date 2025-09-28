using Domain.Entities;
using Domain.ValueObjects;

namespace Application.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public AuthorId AuthorId { get; set; } 
    public DateTime ExpiresAt { get; set; }
}