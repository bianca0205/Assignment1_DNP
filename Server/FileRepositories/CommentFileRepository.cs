using System.Text.Json;
using RepositoryContracts;
using Entities;


namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    private async Task<List<Comment>> ReadCommentsFromFileAsync()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
    }
    
    private async Task SaveCommentsAsync(List<Comment> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        var comments = await ReadCommentsFromFileAsync();
        
        int maxId = comments.Count > 0 ? comments.Max(c => c.CommentId) : 1;
        comment.CommentId = maxId + 1;
        comments.Add(comment);
        await SaveCommentsAsync(comments);
        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!; 
        return comments.AsQueryable();
    }
    
    public async Task<Comment> GetSingleAsync(int id)
    {
        var comments = await ReadCommentsFromFileAsync();
        Comment? comment = comments.FirstOrDefault(c => c.CommentId == id);
    
        if (comment == null)
        {
            throw new InvalidOperationException($"Comment with ID {id} not found");
        }
    
        return comment;
    }
    
    public async Task UpdateAsync(Comment comment)
    {
        var comments = await ReadCommentsFromFileAsync();

        Comment? existingComment = comments.FirstOrDefault(c => c.CommentId == comment.CommentId);
        if (existingComment == null)
        {
            throw new InvalidOperationException($"Comment with ID {comment.CommentId} not found");
        }

        int index = comments.IndexOf(existingComment);
        comments[index] = comment;

        await SaveCommentsAsync(comments);
    }
    
    public async Task DeleteAsync(int id)
    {
        var comments = await ReadCommentsFromFileAsync();
        
        Comment? commentToRemove = comments.FirstOrDefault(c => c.CommentId == id);
        if (commentToRemove == null)
        {
            throw new InvalidOperationException($"Comment with ID {id} not found");
        }
    
        comments.Remove(commentToRemove);
        await SaveCommentsAsync(comments);
    }
    
    
    
    
    
}