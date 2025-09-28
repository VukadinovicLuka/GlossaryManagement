using Application.Commands;
using Application.DTOs;
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

    public TermController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> CreateTerm([FromBody] CreateTermDto request)
    {
        try
        {
            var authorIdClaim = User.FindFirst("authorId")?.Value;
            if (string.IsNullOrEmpty(authorIdClaim))
                return Unauthorized(new { message = "Author ID not found in token" });

            var authorId = AuthorId.Create(Guid.Parse(authorIdClaim));
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
}