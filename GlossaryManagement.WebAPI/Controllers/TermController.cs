using Application.Commands;
using Application.DTOs;
using Application.Queries;
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
    [HttpPost("/create")]
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

    [Authorize]
    [HttpPut("/archive/{id}")]
    public async Task<IActionResult> ArchiveTerm(Guid id)
    {
        try
        {
            var termId = TermId.Create(id);
            var currentAuthorId = _currentUserService.GetCurrentAuthorId();
            var command = new ArchiveTermCommand(termId, currentAuthorId);
        
            await _mediator.Send(command);
            return Ok(new { message = "Term archived successfully" });
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

    [Authorize]
    [HttpDelete("/delete/{id}")]
    public async Task<IActionResult> DeleteTerm(Guid id)
    {
        try
        {
            var termId = TermId.Create(id);
            var currentAuthorId = _currentUserService.GetCurrentAuthorId();
            var command = new DeleteTermCommand(termId, currentAuthorId);
        
            await _mediator.Send(command);
            return Ok(new { message = "Term deleted successfully" });
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
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetPublishedTerms()
    {
        try
        {
            var query = new GetTermsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving terms", details = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTermById(Guid id)
    {
        try
        {
            var termId = TermId.Create(id);
            var currentAuthorId = _currentUserService.GetCurrentAuthorId();
            var query = new GetTermByIdQuery(termId, currentAuthorId);
            var result = await _mediator.Send(query);
            
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the term", details = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("my-terms")]
    public async Task<IActionResult> GetMyTerms()
    {
        try
        {
            var currentAuthorId = _currentUserService.GetCurrentAuthorId();
            var query = new GetTermsByAuthorIdQuery(currentAuthorId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving your terms", details = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetTermsByStatus(string status)
    {
        try
        {
            if (!Enum.TryParse<TermStatus>(status, true, out var termStatus))
                return BadRequest(new { message = "Invalid status. Valid values are: Draft, Published, Archived" });

            var currentAuthorId = _currentUserService.GetCurrentAuthorId();
            var query = new GetTermsByStatusQuery(termStatus, currentAuthorId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving terms by status", details = ex.Message });
        }
    }
}