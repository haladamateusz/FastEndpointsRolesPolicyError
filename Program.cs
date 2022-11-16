using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services
    .AddFastEndpoints()
    .AddAuthenticationJWTBearer("JwtTokenSigningKey");

builder.Services
    .AddSwaggerDoc(maxEndpointVersion: 1, shortSchemaNames: true, settings: s =>
    {
        s.DocumentName = "MinimalApp";
        s.Title = "MinimalApp API";
        s.Version = "1.0";
    });

builder.Services.AddAuthorization(o => o
    .AddPolicy("UsersOnly", x => x.RequireRole("User").RequireClaim("UserID"))
);

builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddDefaultPolicy(p => p
        .WithOrigins(builder.Configuration.GetValue<string>("AllowedOrigin"))
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
    c.Versioning.Prefix = "v";
    c.Versioning.PrependToRoute = true;
});

if (app.Environment.IsDevelopment())
{
    app
        .UseOpenApi()
        .UseSwaggerUi3(c => c.ConfigureDefaults());
}

app.Run();
