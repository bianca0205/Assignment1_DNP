using System.ComponentModel.DataAnnotations;
using WebAPI.Services;
using Entities;

namespace WebAPI.Services;
public class AuthService : IAuthService
{
    private readonly IList<User> users =
    [
        new()
        {
            UserName = "Admin",
            Password = "Admin",
        },
        new()
        {
            UserName = "User",
            Password = "User",
        }
    ];

    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u =>
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)) ?? throw new Exception("User not found");

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here

        // save to persistence instead of list

        users.Add(user);

        return Task.CompletedTask;
    }
}
