using Entities;
namespace RepositoryContracts;

public class ICommentRepository
{
    Task<Comment> AddAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    Task UpdateAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<Comment> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    IQueryable<Comment> GetManyAsync()
    {
        throw new NotImplementedException();
    }
}