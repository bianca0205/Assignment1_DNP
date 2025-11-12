using System.Text.Json;
using ApiContracts.Comment;

namespace BlazorApp.Services.Comment;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient httpClient;

    public HttpCommentService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<CommentDto> AddCommentAsync(CreateCommentDto comment)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("comments", comment);
        response.EnsureSuccessStatusCode();

        CommentDto created = await response.Content.ReadFromJsonAsync<CommentDto>();
        return created;
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByPostIdAsync(
        int postId)
    {
        HttpResponseMessage httpResponse =
            await httpClient.GetAsync($"posts/{postId}/comments");
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<IEnumerable<CommentDto>>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage httpResponse =
            await httpClient.DeleteAsync($"comments/{id}");

        if (!httpResponse.IsSuccessStatusCode)
        {
            string response = await httpResponse.Content.ReadAsStringAsync();
            throw new Exception(response);
        }
    }
}