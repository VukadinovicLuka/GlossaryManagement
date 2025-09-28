using Application.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

public class DeleteTermHandler : IRequestHandler<DeleteTermCommand>
{
    private readonly ITermRepository _termRepository;

    public DeleteTermHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    public async Task Handle(DeleteTermCommand request, CancellationToken cancellationToken)
    {
    
        var term = await _termRepository.GetByIdAsync(request.TermId);
        if (term == null)
        {
            throw new InvalidOperationException("Term not found");
        }
        
        if (term.AuthorId != request.AuthorId)
        {
            throw new UnauthorizedAccessException("You can only delete your own terms");
        }

        if (!term.Delete())
        {
            throw new InvalidOperationException("In order to delete the term it must be in draft status");
        }

        _termRepository.DeleteAsync(term);
    }
}