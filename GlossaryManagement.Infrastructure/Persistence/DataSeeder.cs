using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastucture.Persistence;

public class DataSeeder
{
    public static async Task SeedData(GlossaryDbContext context)
    {
        await SeedAuthors(context);
        await SeedTerms(context);
    }
    private static async Task SeedAuthors(GlossaryDbContext glossaryDbContext)
    {
        if (glossaryDbContext.Authors.Any())
        {
            return;
        }

        var authors = new[]
        {
            Author.Create("admin@gmail.com", "admin"),
            Author.Create("admin2@gmail.com", "admin2"),
        };
        
        await glossaryDbContext.Authors.AddRangeAsync(authors);
        await glossaryDbContext.SaveChangesAsync();
    }

    private static async Task SeedTerms(GlossaryDbContext glossaryDbContext)
    {
        if (glossaryDbContext.Terms.Any())
        {
            return;
        }
        
        var authors = glossaryDbContext.Authors.ToList();
        var authorId1 = authors.First().Id;
        var authorId2 = authors.Skip(1).First().Id;

        var terms = new[]
        {
            Term.Create("abyssal plain", "The ocean floor offshore from the continental margin, usually very flat with a slight slope.",authorId1),
            Term.Create("accrete", "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass.",authorId2 ),
            Term.Create("alkaline", "Term pertaining to a highly basic, as opposed to acidic, substance. For example, hydroxide or carbonate of sodium or potassium.",authorId1)
        };
        
        await glossaryDbContext.Terms.AddRangeAsync(terms);
        await glossaryDbContext.SaveChangesAsync();
    }
}