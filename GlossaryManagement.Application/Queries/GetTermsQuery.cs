using Application.DTOs;
using Domain.ValueObjects;
using MediatR;

namespace Application.Queries;

public class GetTermsQuery : IRequest<IEnumerable<TermDto>>
{
    public TermStatus Status { get; }
    
    public GetTermsQuery(TermStatus status = TermStatus.Published)
    {
        Status = status;
    }
}