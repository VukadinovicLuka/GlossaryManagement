using Application.DTOs;
using Domain.ValueObjects;
using MediatR;

namespace Application.Queries;

public class GetTermsByStatusQuery : IRequest<IEnumerable<TermDto>>
{
    public TermStatus Status { get; }
    public AuthorId AuthorId { get; }

    public GetTermsByStatusQuery(TermStatus status, AuthorId authorId)
    {
        Status = status;
        AuthorId = authorId;
    }
}