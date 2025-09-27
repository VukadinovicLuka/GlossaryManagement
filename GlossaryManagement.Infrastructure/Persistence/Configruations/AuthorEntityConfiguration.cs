using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.Persistence.Configruations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        
        builder.ToTable("Authors");
        
        builder.HasKey(a=>a.Id);
        
        builder.Property(a=>a.Id)
            .HasConversion(id=>id.Value, value=>AuthorId.Create(value))
            .IsRequired();
        
        builder.Property(a=>a.Email)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(a=>a.Password)
            .IsRequired();
    }
}