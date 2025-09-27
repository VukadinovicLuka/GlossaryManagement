using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.Persistence.Configruations;

public class TermEntityConfiguration : IEntityTypeConfiguration<Term>
{
    public void Configure(EntityTypeBuilder<Term> builder)
    {
        builder.ToTable("Terms");
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .HasConversion(id=>id.Value,value =>TermId.Create(value))
            .IsRequired();

        builder.Property(t => t.AuthorId)
            .HasConversion(id => id.Value, value => AuthorId.Create(value))
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(300)
            .IsRequired();
        
        builder.Property(t => t.Name)
            .IsRequired();
        
        builder.Property(t=>t.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(t=>t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.PublishedAt)
            .IsRequired(false);
        
        builder.HasIndex(t => t.AuthorId);
    }
}