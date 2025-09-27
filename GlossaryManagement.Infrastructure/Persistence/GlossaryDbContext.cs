using Domain.Entities;
using Infrastucture.Persistence.Configruations;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Persistence;

public class GlossaryDbContext : DbContext
{
    public GlossaryDbContext(DbContextOptions<GlossaryDbContext> options) : base(options) { }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Term> Terms { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TermEntityConfiguration());
    }
}