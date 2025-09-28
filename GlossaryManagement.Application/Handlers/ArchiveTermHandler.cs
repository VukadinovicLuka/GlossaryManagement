using Application.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

public class ArchiveTermHandler : IRequestHandler<ArchiveTermCommand>
{
    private readonly ITermRepository _termRepository;

    public ArchiveTermHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    public async Task Handle(ArchiveTermCommand request, CancellationToken cancellationToken)
    {
    
        var term = await _termRepository.GetByIdAsync(request.TermId);
        if (term == null)
        {
            throw new InvalidOperationException("Term not found");
        }
        
        if (term.AuthorId != request.AuthorId)
        {
            throw new UnauthorizedAccessException("You can only archive your own terms");
        }

        term.Archive();
    
        await _termRepository.UpdateAsync(term);
    }
}