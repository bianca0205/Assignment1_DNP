using ApiContracts;

namespace BlazorApp.Services.User;

public interface IUserService
{
    Task<UserDto> AddUserAsync(CreateUserDto request);
    Task<UserDto> GetUserByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task UpdateUserAsync(int id, UpdateUserDto request);
    Task DeleteUserAsync(int id);
}