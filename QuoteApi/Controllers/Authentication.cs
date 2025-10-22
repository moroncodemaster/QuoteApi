using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace QuoteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Authentication : ControllerBase
{
    IConfiguration _configuration;

    public Authentication(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // GET
    [HttpGet]
    public IActionResult GetAuthToken(string username, string password)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Anonymous, "Anonymous"),
            new Claim(ClaimTypes.Name, username),
            // new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            // new Claim(ClaimTypes.Email, user.Email),
            // Add other relevant claims like roles
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), // Set token expiration
            signingCredentials: creds
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { Token = jwtToken });
    }
}