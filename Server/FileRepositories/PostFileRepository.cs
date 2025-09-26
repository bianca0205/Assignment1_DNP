using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";
    
    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    private async Task<List<Post>> ReadPostsFromFileAsync()
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
    }
    
    private async Task SavePostsAsync(List<Post> posts)
    {
        string postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(filePath, postsAsJson);
    }

    public async Task UpdateAsync(Post post)
    {
        var posts = await ReadPostsFromFileAsync();
        
        Post? existingPost = posts.FirstOrDefault(p => p.PostId == post.PostId);
        if (existingPost is null)
        {
            throw new Exception("Post not found");
        }
        int index = posts.IndexOf(existingPost);
        posts[index] = post;

        await SavePostsAsync(posts);
        
    }

    public async Task DeleteAsync (int id)
    {
        var posts = await ReadPostsFromFileAsync();
        
        Post? postToRemove = posts.FirstOrDefault(p => p.PostId == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException($"Post not found");
        }
        
        posts.Remove(postToRemove);
        await SavePostsAsync(posts);
    }
    
    public async Task<Post> AddAsync(Post post)
    {
        var posts = await ReadPostsFromFileAsync();
        
        int maxId = posts.Count > 0 ? posts.Max(c => c.PostId) : 1;
        post.PostId = maxId + 1;
        posts.Add(post);
        await SavePostsAsync(posts);
        return post;
    }
    public async Task<Post> GetSingleAsync(int id)
    {
        var posts = await ReadPostsFromFileAsync();
        
        Post? post = posts.FirstOrDefault(p => p.PostId == id);
        if (post is null)
        {
            throw new InvalidOperationException($"Post with ID {id} not found");
        }
        return post;
    }
    public IQueryable<Post> GetManyAsync()
    {  
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;
       List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson);
       return posts.AsQueryable();
    }
}