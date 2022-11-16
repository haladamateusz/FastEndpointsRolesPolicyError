namespace MinimalFastEndpoints.Sample;

public class GetSampleListResponse
{
    public GetSampleListResponse(string content)
    {
        Content = content;
    }

    public string Content { get; set; }

}