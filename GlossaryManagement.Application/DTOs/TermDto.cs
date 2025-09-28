using Domain.ValueObjects;

namespace Application.DTOs;

public class TermDto
{
    public TermId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TermStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public TermDto(TermId id, string name, string description, TermStatus status, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
    }
}