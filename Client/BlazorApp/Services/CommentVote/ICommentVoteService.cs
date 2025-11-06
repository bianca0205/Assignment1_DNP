using ApiContracts.CommentVote;

namespace BlazorApp.Services.CommentVote;

public interface ICommentVoteService
{
    Task<CommentVoteDto> AddCommentVoteAsync(CreateCommentVoteDto request);
    Task<IEnumerable<CommentVoteDto>> GetCommentVotesByPostIdAsync(int postId);
    Task DeleteCommentVoteAsync(int id);
}