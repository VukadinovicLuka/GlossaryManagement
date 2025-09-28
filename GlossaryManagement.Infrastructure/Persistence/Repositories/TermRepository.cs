using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Persistence.Repositories;

public class TermRepository : ITermRepository
{

    private readonly GlossaryDbContext _context;

    public TermRepository(GlossaryDbContext context)
    {
        _context = context;
    }
    public async Task SaveAsync(Term term)
    {
        await _context.Terms.AddAsync(term);
        await _context.SaveChangesAsync();

    }

    public async Task<Term?> GetByNameAsync(string name)
    {
        return await _context.Terms.FirstOrDefaultAsync(t => t.Name == name);
    }
}