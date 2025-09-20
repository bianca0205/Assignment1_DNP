using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync(IUserRepository userRepository,
        IPostRepository postRepository)
    {
        Console.WriteLine("Write your comment below:");
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

        Console.Write("Enter post ID: ");
        if (!int.TryParse(Console.ReadLine(), out int postId))
        {
            Console.WriteLine("You must enter a valid post ID");
            return;
        }

        try
        {
            await postRepository.GetSingleAsync(postId);
            await userRepository.GetSingleAsync(userId);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("User or Post with the given ID does not exist.");
            return;
        }

        var newComment = new Entities.Comment
        {
            Body = body,
            UserId = userId,
            PostId = postId
        };
        
        var createdComment = await commentRepository.AddAsync(newComment);
        Console.WriteLine($"Comment created successfully with ID: {createdComment.CommentId}");
    }
}