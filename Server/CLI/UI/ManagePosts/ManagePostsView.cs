using RepositoryContracts;
using  Entities;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public ManagePostsView(IPostRepository postRepository,
        IUserRepository userRepository)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            ShowPostMenu();
            var choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    await CreatePostAsync();
                    break;
                case "2":
                    await ListPostsAsync();
                    break;
                case "3":
                    await ViewPostAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
    private void ShowPostMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Post Management ===");
        Console.WriteLine("1. Create Post");
        Console.WriteLine("2. List All Posts");
        Console.WriteLine("3. View Post");
        Console.WriteLine("0. Back to Main Menu");
        Console.Write("Choose an option: ");
    }
    
    private async Task CreatePostAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create New Post ===");

        var createPostView = new CreatePostView(postRepository);
        await createPostView.ShowAsync(userRepository);
    }
    private async Task ListPostsAsync()
    {
        var listPostsView = new ListPostsView_cs(postRepository);
        await listPostsView.ShowAsync(userRepository);
    }
    private async Task ViewPostAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Post ===");
        Console.Write("Enter Post ID: ");
        if (!int.TryParse(Console.ReadLine(), out int postId))
        {
            Console.WriteLine("You must enter a valid post ID");
            return;
        }

        var singlePostView = new SinglePostView(postRepository);
        await singlePostView.ShowAsync(postId,userRepository);
    }
}