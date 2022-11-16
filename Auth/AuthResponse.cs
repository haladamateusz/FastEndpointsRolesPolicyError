namespace MinimalFastEndpoints.Auth;

public class AuthResponse
{
    public string UserName { get; set; }
    public string Token { get; set; }

    public AuthResponse(string userName, string token)
    {
        UserName = userName;
        Token = token;
    }
}