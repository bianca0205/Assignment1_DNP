using ApiContracts.Post;

namespace BlazorApp.Services.Post;

public interface IPostService
{
    Task<PostDto> CreatePostAsync(CreatePostDto request);
    Task<PostDto> GetPostByIdAsync(int id);
    Task<IEnumerable<PostDto>> GetPostsAsync();
    Task UpdatePostAsync(int id, UpdatePostDto request);
    Task DeletePostAsync(int id);
}