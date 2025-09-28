using Application.Commands;
using Application.DTOs;
using Application.Services;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryManagement.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TermController : ControllerBase
{
    
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public TermController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateTerm([FromBody] CreateTermDto request)
    {
        try
        {
            var authorId = _currentUserService.GetCurrentAuthorId();
            var command = new CreateTermCommand(request.Name, request.Description, authorId);
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message }); 
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message }); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred", details = ex.Message });
        }
    }

    [Authorize]
    [HttpPut("/publish/{id}")]
    public async Task<IActionResult> PublishTerm(Guid id)
    {
        try
        {
            var termId = TermId.Create(id);
            var currentAuthorId = _currentUserService.GetCurrentAuthorId();
            var command = new PublishTermCommand(termId, currentAuthorId);
        
            await _mediator.Send(command);
            return Ok(new { message = "Term published successfully" });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}