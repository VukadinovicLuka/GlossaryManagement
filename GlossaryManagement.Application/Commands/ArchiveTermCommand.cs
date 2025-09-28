using Domain.ValueObjects;
using MediatR;

namespace Application.Commands;

public class ArchiveTermCommand : IRequest
{
    public TermId TermId { get;}
    public AuthorId AuthorId { get;}

    public ArchiveTermCommand(TermId termId, AuthorId authorId)
    {
        TermId = termId;
        AuthorId = authorId;
    }
}