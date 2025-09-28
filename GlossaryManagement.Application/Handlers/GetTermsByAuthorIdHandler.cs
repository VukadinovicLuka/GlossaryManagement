using Application.DTOs;
using Application.Queries;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

public class GetTermsByAuthorIdHandler : IRequestHandler<GetTermsByAuthorIdQuery, IEnumerable<TermDto>>
{
    private readonly ITermRepository _termRepository;

    public GetTermsByAuthorIdHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    public async Task<IEnumerable<TermDto>> Handle(GetTermsByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        var terms = await _termRepository.GetByAuthorIdAsync(request.AuthorId);

        if (!terms.Any())
        {
            throw new NullReferenceException("No terms found");
        }
        
        return terms.Select(t => new TermDto(t.Id, t.Name, t.Description, t.Status, t.CreatedAt));
    }
}