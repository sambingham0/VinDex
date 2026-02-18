using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using VinDex.Api.Services;

namespace VinDex.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("google-login")]
    [AllowAnonymous]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Credential))
        {
            return BadRequest(new { Message = "Credential is required." });
        }

        try
        {
            var token = await _authService.LoginWithGoogleAsync(request.Credential);
            return Ok(new { Token = token });
        }
        catch (InvalidJwtException ex)
        {
            return Unauthorized(new { Message = "Invalid Google credential", Error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { Message = "Login failed due to server error", Error = ex.Message });
        }
    }

}

public record GoogleLoginRequest(string Credential);