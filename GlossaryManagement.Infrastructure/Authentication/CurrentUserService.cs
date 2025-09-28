using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Application.Services;

namespace Infrastructure.Authentication
{

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthorId GetCurrentAuthorId()
        {
            var authorIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst("authorId")?.Value;

            if (string.IsNullOrEmpty(authorIdClaim))
                throw new UnauthorizedAccessException("Author ID not found in token");

            return AuthorId.Create(Guid.Parse(authorIdClaim));
        } 
    }
}