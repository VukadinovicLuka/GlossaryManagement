using Application.Commands;
using Application.DTOs;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryManagement.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IAuthenticationService _authenticationService;
    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _authenticationService.LoginAsync(loginDto);
            if(result==null) Unauthorized(new {message = "Username or password is incorrect."});
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occured during login.", error = ex.Message});
        }
        
    }
}