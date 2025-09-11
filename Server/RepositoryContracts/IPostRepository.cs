using Entities;

namespace RepositoryContracts;

public class IPostRepository
{
    Task<Post> AddSync(Post post)
    {
        throw new NotImplementedException();
    }

    Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }

    Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<Post> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    IQueryable<Post> GetManyAsync()
    {
        throw new NotImplementedException();
    }
}