using System.Security.Claims;
using Domain.Entities;

namespace Domain.Repositories;

public interface IJwtService
{
    string GenerateToken(Author author);
    ClaimsPrincipal? ValidateToken(string token);
}