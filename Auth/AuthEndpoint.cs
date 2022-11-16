namespace MinimalFastEndpoints.Auth;

public class AuthEndpoint : Endpoint<AuthRequest, AuthResponse>
{
    public override void Configure()
    {
        Post("/authenticate");
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(AuthRequest request, CancellationToken ct)
    {
        var token = JWTBearer.CreateToken(
            "JwtTokenSigningKey",
            DateTime.UtcNow.AddDays(1),
            claims: new[] { ("Username", request.UserName), ("UserID", "001") },
            roles: new[] { "Admin", "User" },
            permissions: new[] { "ManageInventory", "ManageUsers" });

        var response = new AuthResponse(request.UserName, token);

        await SendAsync(response, cancellation: ct);
    }
}