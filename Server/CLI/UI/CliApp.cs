using RepositoryContracts;
using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;
    
    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task StartAsync()
    {
        while (true)
        {
            ShowMainMenu();
            var choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1":
                        var userView = new ManageUsersView(userRepository);
                        await userView.ShowAsync();
                        break;
                    case "2":
                        var postView = new ManagePostsView(postRepository,
                            userRepository);
                        await postView.ShowAsync();
                        break;
                    case "3":
                        var commentView = new ManageCommentsView(
                            commentRepository, postRepository, userRepository);
                        await commentView.ShowAsync();
                        break;
                    case "0":
                        Console.WriteLine("Exiting application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
    private void ShowMainMenu()
    {
        Console.WriteLine("=== Main Menu ===");
        Console.WriteLine("1. Manage Users");
        Console.WriteLine("2. Manage Posts");
        Console.WriteLine("3. Manage Comments");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }
}