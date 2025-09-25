using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    
    public ManageCommentsView(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            ShowCommentMenu();
            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    await CreateCommentAsync();
                    break;
                case "2":
                    await ListCommentsAsync();
                    break;
                case "3":
                    await ViewCommentAsync();
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
    private void ShowCommentMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Comment Management ===");
        Console.WriteLine("1. Create Comment");
        Console.WriteLine("2. List All Comments");
        Console.WriteLine("3. View Comment");
        Console.WriteLine("0. Back to Main Menu");
        Console.Write("Choose an option: ");
    }

    private async Task CreateCommentAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create New Comment ===");
        
        var createCommentView = new CreateCommentView(commentRepository);
        await createCommentView.ShowAsync(userRepository, postRepository);
    }

    private async Task ListCommentsAsync()
    {
        var listCommentsView = new ListCommentsView(commentRepository);
        await listCommentsView.ShowAsync();
    }
    private async Task ViewCommentAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Comment ===");

        Console.Write("Enter Comment ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("You must enter a valid comment ID");
            return;
        }

        var singleCommentView = new SingleCommentView(commentRepository);
        await singleCommentView.ShowAsync(postRepository, userRepository, id);
    }
    
}