using ApiContracts.Comment;

namespace BlazorApp.Services.Comment;

public interface ICommentService
{
    Task<CommentDto> AddCommentAsync(CreateCommentDto request);
    Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(int postId);
    Task DeleteCommentAsync(int id);
}