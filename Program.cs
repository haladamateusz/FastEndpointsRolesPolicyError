using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddFastEndpoints();
builder.Services.AddAuthenticationJWTBearer("JwtTokenSigningKey");
builder.Services.AddAuthorization(o => o
    .AddPolicy("UsersOnly", x => x.RequireRole("User")
    .RequireClaim("UserID")));
builder.Services.AddSwaggerDoc(maxEndpointVersion: 1, shortSchemaNames: true, settings: s =>
{
    s.DocumentName = "MinimalApp";
    s.Title = "MinimalApp API";
    s.Version = "1.0";
});

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddDefaultPolicy(p => p
        .WithOrigins(builder.Configuration.GetValue<string>("AllowedOrigin"))
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
    c.Versioning.Prefix = "v";
    c.Versioning.PrependToRoute = true;
});
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.Run();
