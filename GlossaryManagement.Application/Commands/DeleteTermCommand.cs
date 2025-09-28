using Domain.ValueObjects;
using MediatR;

namespace Application.Commands;

public class DeleteTermCommand : IRequest
{
    public TermId TermId { get;}
    public AuthorId AuthorId { get;}

    public DeleteTermCommand(TermId termId, AuthorId authorId)
    {
        TermId = termId;
        AuthorId = authorId;
    }
}