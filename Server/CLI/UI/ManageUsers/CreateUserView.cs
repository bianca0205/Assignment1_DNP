using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository userRepository;
    
    public CreateUserView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine("=== Create New User ===");
        
        Console.Write("Enter username: ");
        var userName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userName) )
        {
            Console.WriteLine("Username cannot be empty.");
            return;
        }
        
        var existingUsers = userRepository.GetManyAsync().Where(u=> u.UserName.Equals(userName));
        if (existingUsers.Any())
        {
            Console.WriteLine(
                "Username already exists. Please choose a different username.");
            return;
        }
        
        Console.Write("Password: ");
        var password = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password cannot be empty.");
            return;
        }
        
        var newUser = new User
        {
            UserName = userName,
            Password = password
        };
        var created = await userRepository.AddAsync(newUser);
        Console.WriteLine($"User created successfully with ID: {created.UserId}");
    }

}