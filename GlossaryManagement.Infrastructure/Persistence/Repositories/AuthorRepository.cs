using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Persistence.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly GlossaryDbContext _context;

    public AuthorRepository(GlossaryDbContext context)
    {
        _context = context;
    }
    public async Task<Author?> GetByEmailAsync(string email)
    {
        return await _context.Authors
            .FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
    }
}