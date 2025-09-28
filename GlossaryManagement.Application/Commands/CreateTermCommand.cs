using Application.DTOs;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands;

public class CreateTermCommand : IRequest<TermDto>
{
    public string Name { get;}
    public string Description { get;}
    public AuthorId AuthorId { get; }

    public CreateTermCommand(string name, string description, AuthorId authorId)
    {
        Name = name;
        Description = description;
        AuthorId = authorId;
    }
}