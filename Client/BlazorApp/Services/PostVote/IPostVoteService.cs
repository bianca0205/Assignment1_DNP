using ApiContracts.PostVote;

namespace BlazorApp.Services.PostVote;

public interface IPostVoteService
{
    Task<PostVoteDto> CreatePostVoteAsync(CreatePostVoteDto request);
    Task<PostVoteDto> GetPostVoteByPostIdAsync(int postId);
    Task DeletePostVoteAsync(int postId);
}