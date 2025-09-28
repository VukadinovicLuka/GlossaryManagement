using Application.Commands;
using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Handlers;

public class CreateTermHandler : IRequestHandler<CreateTermCommand, TermDto>
{
    private readonly ITermRepository _termRepository;

    public CreateTermHandler(ITermRepository termRepository)
    {
        _termRepository = termRepository;
    }

    public async Task<TermDto> Handle(CreateTermCommand request, CancellationToken cancellationToken)
    {
        
        var existingTermWithSameName = await _termRepository.GetByNameAsync(request.Name);
        
        if (existingTermWithSameName != null)
        {
            throw new InvalidOperationException("A term with this name already exists");
        }
        
        var term = Term.Create(request.Name, request.Description, request.AuthorId);

        await _termRepository.SaveAsync(term);
    
        return new TermDto(term.Id, term.Name, term.Description, term.Status, term.CreatedAt);
    }
}