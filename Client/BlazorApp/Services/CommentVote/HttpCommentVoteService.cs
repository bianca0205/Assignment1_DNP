using System.Text.Json;
using ApiContracts.CommentVote;

namespace BlazorApp.Services.CommentVote;

public class HttpCommentVoteService : ICommentVoteService
{
    private readonly HttpClient client;

    public HttpCommentVoteService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<CommentVoteDto> AddCommentVoteAsync(
        CreateCommentVoteDto request)
    {
        HttpResponseMessage response =
            await client.PostAsJsonAsync("api/v1/CommentVote", request);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        return JsonSerializer.Deserialize<CommentVoteDto>(responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task<IEnumerable<CommentVoteDto>> GetCommentVotesByPostIdAsync(
        int postId)
    {
        HttpResponseMessage response =
            await client.GetAsync($"api/v1/CommentVote/post/{postId}");
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        return JsonSerializer.Deserialize<IEnumerable<CommentVoteDto>>(
            responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task DeleteCommentVoteAsync(int id)
    {
        HttpResponseMessage response =
            await client.DeleteAsync($"api/v1/CommentVote/{id}");

        if (!response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            throw new Exception(responseContent);
        }
    }
}