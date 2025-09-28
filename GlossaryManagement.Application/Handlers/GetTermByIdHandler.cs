using Application.DTOs;
using Application.Queries;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

public class GetTermByIdHandler : IRequestHandler<GetTermByIdQuery, TermDto?>
{
    private readonly ITermRepository _termRepository;

    public GetTermByIdHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    public async Task<TermDto?> Handle(GetTermByIdQuery request, CancellationToken cancellationToken)
    {
        var term = await _termRepository.GetByIdAsync(request.TermId);
        
        if (term==null)
        {
            throw new NullReferenceException("No term found");
        }
        
        if (term.AuthorId != request.AuthorId)
        {
            throw new UnauthorizedAccessException("You can only get your own terms");
        }
        
        return term != null ? new TermDto(term.Id, term.Name, term.Description, term.Status, term.CreatedAt) : null;
    }
}