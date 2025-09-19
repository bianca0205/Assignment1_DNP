using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    
    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync(int postId,IUserRepository userRepository)
    {
        Console.Clear();
        Console.WriteLine("=== View Post ===");

        try
        {
            var post= await postRepository.GetSingleAsync(postId); 
            var author = await userRepository.GetSingleAsync(post.UserId);
            
            Console.WriteLine($"Post ID: {post.PostId}");
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Body: {post.Body}");
            Console.WriteLine($"Author: {author.UserName} (ID: {author.UserId})");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Post with the given ID does not exist.");
        }
    }
}