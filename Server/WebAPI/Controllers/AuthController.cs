using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositoryContracts;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IConfiguration config, IUserRepository userRepository)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        // find user by username
        User? user =
            await userRepository.GetByUsernameAsync(loginRequest.UserName);
        
        // if the user doesn't exist, return Unauthorized
        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }
        
        // if the password is incorrect, return Unauthorized
        if (user.Password != loginRequest.Password)
        {
            return Unauthorized("Invalid username or password");
        }
        
        // convert the user to a userdto
        UserDto userDto = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName
        };
        return Ok(userDto);
    }

    private string GenerateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? "");

        List<Claim> claims = GenerateClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256
            ),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"] ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("Username", user.UserName),
            new Claim("Password", user.Password)
        };
        return [.. claims];
    }
}