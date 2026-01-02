using Microsoft.AspNetCore.Mvc;
using WebStoreUser.Application.Interfaces.Services;
using WebStoreUser.Application.Requests;

namespace WebStoreUser.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        var isSuccess = await authService.RegisterAsync(request);
        if (!isSuccess)
            return BadRequest("User already exists");

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        if (result == null)
            return BadRequest("Invalid credentials");

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await authService.RefreshAsync(request);
        if (result == null)
            return Unauthorized("Invalid refresh token");

        return Ok(result);
    }
}
