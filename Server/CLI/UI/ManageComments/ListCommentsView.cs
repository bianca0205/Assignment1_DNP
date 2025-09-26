using Entities;
using RepositoryContracts;
namespace CLI.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository commentRepository;
    
    public ListCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task ShowAsync()
    {
        Console.Clear();
        Console.WriteLine("=== List of Comments ===");
        
        var comments =  commentRepository.GetMany().ToList();
        if (comments.Count == 0)
        {
            Console.WriteLine("No comments available.");
            return;
        }

        foreach (var comment in comments)
        {
            var bodyDisplay = comment.Body.Length > 30
                ? comment.Body.Substring(0, 30) + "..." 
                : comment.Body;
            Console.WriteLine($"Comment ID: {comment.CommentId}, Body: {bodyDisplay}, Post ID: {comment.PostId}, User ID: {comment.UserId}");
        }
    }
}