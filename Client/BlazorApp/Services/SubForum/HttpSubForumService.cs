using System.Text.Json;
using ApiContracts.SubForum;

namespace BlazorApp.Services.SubForum;

public class HttpSubForumService : ISubForumService
{
    private readonly HttpClient client;

    public HttpSubForumService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<SubForumDto> CreateSubForumAsync(
        CreateSubForumDto request)
    {
        HttpRequestMessage requestMessage =
            new HttpRequestMessage(HttpMethod.Post, "api/subforum");
        requestMessage.Content = JsonContent.Create(request);

        HttpResponseMessage response = await client.SendAsync(requestMessage);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        return JsonSerializer.Deserialize<SubForumDto>(responseContent,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task<SubForumDto> GetSubForumByIdAsync(int subForumId)
    {
        HttpResponseMessage httpResponse =
            await client.GetAsync(
                $"api/subforum/{subForumId}");
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }

        return JsonSerializer.Deserialize<SubForumDto>(response,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    public async Task DeleteSubForumAsync(int subForumId)
    {
        HttpResponseMessage httpResponse =
            await client.DeleteAsync(
                $"api/subforum/{subForumId}");
        string response = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
    }
}