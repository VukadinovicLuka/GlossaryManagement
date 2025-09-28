using Application.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

public class PublishTermHandler : IRequestHandler<PublishTermCommand>
{
    private readonly ITermRepository _termRepository;

    public PublishTermHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    public async Task Handle(PublishTermCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Handler - Request AuthorId: {request.AuthorId}");
    
        var term = await _termRepository.GetByIdAsync(request.TermId);
        if (term == null)
        {
            Console.WriteLine("Term not found");
            throw new InvalidOperationException("Term not found");
        }

        Console.WriteLine($"Handler - Term AuthorId: {term.AuthorId}");
        Console.WriteLine($"Handler - Term Status: {term.Status}");
        Console.WriteLine($"Handler - Term Description Length: {term.Description.Length}");

        if (term.AuthorId != request.AuthorId)
        {
            Console.WriteLine("Authorization failed - different authors");
            throw new UnauthorizedAccessException("You can only publish your own terms");
        }

        Console.WriteLine("About to call term.Publish()");
        term.Publish();
    
        Console.WriteLine("Term published successfully, saving...");
        await _termRepository.UpdateAsync(term);
    }
}