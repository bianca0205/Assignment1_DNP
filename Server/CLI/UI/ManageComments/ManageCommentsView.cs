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

    // private async Task CreateCommentAsync()
    // {
    //     Console.Clear();
    //     Console.WriteLine("=== Create New Comment ===");
    //     
    //     var createCommentView = new CreateCommentView(commentRepository);
    //     await createCommentView.ShowAsync(postRepository,userRepository);
    // }

}