using System.Windows.Input;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands;

public class PublishTermCommand : IRequest
{
    public TermId TermId { get;}
    public AuthorId AuthorId { get;}

    public PublishTermCommand(TermId termId, AuthorId authorId)
    {
        TermId = termId;
        AuthorId = authorId;
    }
}