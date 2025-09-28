using Application.DTOs;
using Application.Queries;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

public class GetTermsByStatusHandler : IRequestHandler<GetTermsByStatusQuery, IEnumerable<TermDto>>
{
    private readonly ITermRepository _termRepository;

    public GetTermsByStatusHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    public async Task<IEnumerable<TermDto>> Handle(GetTermsByStatusQuery request, CancellationToken cancellationToken)
    {
        var terms = await _termRepository.GetByAuthorIdAndStatusAsync(request.AuthorId,request.Status);
        
        if (!terms.Any())
        {
            throw new NullReferenceException("No terms found");
        }
        
        return terms.Select(t => new TermDto(t.Id, t.Name, t.Description, t.Status, t.CreatedAt));
    }
}