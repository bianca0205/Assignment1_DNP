namespace ApiContracts;

public class LoginRequest(string userName, string password)
{
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
}