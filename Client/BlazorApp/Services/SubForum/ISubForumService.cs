using ApiContracts.SubForum;

namespace BlazorApp.Services.SubForum;

public interface ISubForumService
{
    Task<SubForumDto> CreateSubForumAsync(CreateSubForumDto request);
    Task<SubForumDto> GetSubForumByIdAsync(int subForumId);
    Task DeleteSubForumAsync(int subForumId);
}