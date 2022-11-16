namespace MinimalFastEndpoints.Sample;

public class GetSampleListEndpoint : EndpointWithoutRequest<List<GetSampleListResponse>>
{
    public override void Configure()
    {
        Get("/samples");
        Description(x => x.WithName("GetSamples"));
        Version(1);

        // This works
        Policies("UsersOnly");

        // This don't
        //Claims("UserID");
        //Roles("Admin", "User");
        //Permissions("ManageUsers");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var samples = new List<string>
        {
            "Sample1",
            "Sample2"
        };

        var response = samples
            .Select(content => new GetSampleListResponse(content))
            .ToList();

        await SendAsync(response, cancellation: ct);
    }
}