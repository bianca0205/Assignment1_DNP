using Entities;
namespace RepositoryContracts;

public class IUserRepository
{
    Task <User> AddSync(User user)
    {
        throw new NotImplementedException();
    }

    Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<User> GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }

    IQueryable<User> GetManyAsync()
    {
        throw new NotImplementedException();
    }
}