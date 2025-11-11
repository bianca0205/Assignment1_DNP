using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest loginRequest)
    {
        // Find the user by username
        User? user = await userRepository.GetByUsernameAsync(loginRequest.UserName);

        // If user not found or password mismatch → Unauthorized
        if (user == null || user.Password != loginRequest.Password)
        {
            return Unauthorized("Invalid username or password");
        }

        // Convert to UserDto (no password or sensitive data)
        var userDto = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName
        };

        // Return JSON UserDto
        return Ok(userDto);
    }
}