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
        var listPostsView = new ListPostsView(postRepository);
        await listPostsView.ShowAsync(userRepository);
    }
}