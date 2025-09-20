using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class SingleCommentView
{
    private readonly ICommentRepository commentRepository;
    
    public SingleCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync(IPostRepository postRepository,IUserRepository userRepository,int commentId)
    {
        Console.Clear();
        Console.WriteLine("=== View Comment ===");
        
        try
        {
            var comment = await commentRepository.GetSingleAsync(commentId);
            var post = await postRepository.GetSingleAsync(comment.PostId);
            var user = await userRepository.GetSingleAsync(comment.UserId);
            
            Console.WriteLine($"Comment ID: {comment.CommentId}");
            Console.WriteLine($"Body: {comment.Body}");
            Console.WriteLine($"Post: {post.Title} (ID: {post.PostId})");
            Console.WriteLine($"User: {user.UserName} (ID: {user.UserId})");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
}