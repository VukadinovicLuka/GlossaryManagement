using Application.DTOs;
using Domain.ValueObjects;
using MediatR;

namespace Application.Queries;

public class GetTermsByAuthorIdQuery : IRequest<IEnumerable<TermDto>>
{
    public AuthorId AuthorId { get; }

    public GetTermsByAuthorIdQuery(AuthorId authorId)
    {
        AuthorId = authorId;
    }
}