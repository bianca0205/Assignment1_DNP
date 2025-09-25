using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;
    
    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.Clear();
        Console.WriteLine("=== List of Users ===");
        
        var users = userRepository.GetManyAsync().ToList();
        if (!users.Any())
        {
            Console.WriteLine("No users found.");
            return;
        }
        
        foreach (var user in users)
        {
            Console.WriteLine($"User ID: {user.UserId}, UserName: {user.UserName}");
        }
    }

}