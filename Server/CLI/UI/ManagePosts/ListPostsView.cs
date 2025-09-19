using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView_cs
{
    private readonly IPostRepository postRepository;
    
    public ListPostsView_cs(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync(IUserRepository userRepository)
    {
        Console.Clear();
        Console.WriteLine("=== List of Posts ===");

        var posts = postRepository.GetManyAsync().ToList();
        if (posts.Count == 0)
        {
            Console.WriteLine("No posts available.");
            return;
        }
        foreach (var post in posts)
        {
            User author = await userRepository.GetSingleAsync(post.UserId);
            string title = post.Title.Length > 20 ? post.Title.Substring(0, 20) + "..." : post.Title;
            Console.WriteLine($"Post ID: {post.PostId}, Title: {post.Title}, Author: {author.UserName})");
        }
    }
}