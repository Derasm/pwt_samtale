using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration config, ILogger<AuthController> logger)
    {
        _config = config;
        _logger = logger;
    }

    [HttpGet("request-token")] // No login required, just generates a token
    public IActionResult GetToken()
    {
        _logger.LogInformation("Generating new token token");
        var token = JwtTokenService.GenerateToken(_config["Jwt:Key"], _config["Jwt:Issuer"]);
        return Ok(new { token });
    }
}