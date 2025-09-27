using Domain.ValueObjects;

namespace Domain.Entities;

public class Term
{
    public TermId Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; }
    public DateTime? PublishedAt { get; private set; }
    public TermStatus Status { get; private set; }
    public AuthorId AuthorId { get; }

    private Term(TermId id, string name, string description, AuthorId authorId)
    {
        Id = id;
        Name = name;
        Description = description;
        AuthorId = authorId;
        Status = TermStatus.Draft;
        CreatedAt = DateTime.UtcNow;
        PublishedAt = null;
    }

    public static Term Create(string name, string description, AuthorId authorId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name can not be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description can not be empty", nameof(description));
        }

        if (authorId == null)
        {
            throw new ArgumentNullException(nameof(authorId));
        }
        
        return new Term(TermId.NewId(), name, description, authorId);
    }
}