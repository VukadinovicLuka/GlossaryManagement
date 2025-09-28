using Application.DTOs;
using Application.Queries;
using Domain.Repositories;
using Domain.ValueObjects;
using MediatR;

namespace Application.Handlers;

public class GetTermsHandler : IRequestHandler<GetTermsQuery, IEnumerable<TermDto>>
{
    private readonly ITermRepository _termRepository;

    public GetTermsHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    public async Task<IEnumerable<TermDto>> Handle(GetTermsQuery request, CancellationToken cancellationToken)
    {
        var terms = await _termRepository.GetByStatusAsync(request.Status);
        
        if (!terms.Any())
        {
            throw new NullReferenceException("No published terms found");
        }
        
        return terms.Select(t => new TermDto(t.Id, t.Name, t.Description, t.Status, t.CreatedAt));
    }
}