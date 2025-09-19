using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
  
    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task ShowAsync(IUserRepository userRepository)
    {
        Console.Write("Enter post title: ");
        string title = Console.ReadLine()!;
        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Post title cannot be empty");
            return;
        }
        
        Console.Write("Enter post body: ");
        string body = Console.ReadLine()!;
        if (string.IsNullOrEmpty(body))
        {
            Console.WriteLine("Post body cannot be empty");
            return;
        }
        
        Console.Write("Enter user ID: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("You must enter a valid user ID");
            return;
        }
        
        try
        {
            User user = await userRepository.GetSingleAsync(userId);
            Post newPost = new Post
            {
                Title = title,
                Body = body,
                UserId = userId,
            };
            Post createdPost = await postRepository.AddAsync(newPost);
            Console.WriteLine($"Post created successfully with ID: {createdPost.PostId}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("User with the given ID does not exist.");
        }
    }
}