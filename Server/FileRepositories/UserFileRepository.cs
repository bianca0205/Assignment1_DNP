using Entities;
using RepositoryContracts;
using System.Text.Json;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";
    
    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    private async Task<List<User>> ReadUsersFromFileAsync()
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
    }
    
    private async Task SaveUsersAsync(List<User> users)
    {
        string usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }
    
    public async Task UpdateAsync(User user)
    {
        var users = await ReadUsersFromFileAsync();
        
        User? existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
        if (existingUser is null)
        {
            throw new Exception("User not found");
        }
        int index = users.IndexOf(existingUser);
        users[index] = user;

        await SaveUsersAsync(users);
    }

    public async Task<User> AddAsync(User user)
    {
        var users = await ReadUsersFromFileAsync();
        
        int maxId = users.Any() ? users.Max(u => u.UserId) : 0;
        user.UserId = maxId + 1;
        users.Add(user);
        await SaveUsersAsync(users);
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        var users = await ReadUsersFromFileAsync();
        
        User? userToRemove = users.FirstOrDefault(u => u.UserId == id);
        if (userToRemove == null)
        {
            throw new InvalidOperationException($"Comment with ID {id} not found");
        }
    
        users.Remove(userToRemove);
        await SaveUsersAsync(users);
    }
    public async Task<User> GetSingleAsync(int id)
    {
        var users = await ReadUsersFromFileAsync();
        
        User? user = users.FirstOrDefault(u => u.UserId == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID {id} not found");
        }
        return user;
    }
    public IQueryable<User> GetManyAsync()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!; 
        return users.AsQueryable();
    }
    
}