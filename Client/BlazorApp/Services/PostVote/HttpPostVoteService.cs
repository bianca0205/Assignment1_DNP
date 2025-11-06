using System.Text.Json;
using ApiContracts.PostVote;
using BlazorApp.Services.Post;

namespace BlazorApp.Services.PostVote;

public class HttpPostVoteService : IPostVoteService
{
    private readonly HttpClient client;

    public HttpPostVoteService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<PostVoteDto> CreatePostVoteAsync(
        CreatePostVoteDto request)
    {
        HttpResponseMessage response = client
            .PostAsJsonAsync("api/Post/CreatePostVote", request).Result;
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        return JsonSerializer.Deserialize<PostVoteDto>(responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task<PostVoteDto> GetPostVoteByPostIdAsync(int postId)
    {
        HttpResponseMessage httpResponse =
            await client.GetAsync(
                $"api/Post/GetPostVoteByPostId?postId={postId}");
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<PostVoteDto>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task DeletePostVoteAsync(int postId)
    {
        HttpResponseMessage httpResponse =
            await client.DeleteAsync(
                $"api/Post/DeletePostVote?postId={postId}");
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }
}