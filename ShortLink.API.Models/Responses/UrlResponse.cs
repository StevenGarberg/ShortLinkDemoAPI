namespace ShortLink.API.Models.Responses;

public class UrlResponse
{
    public string Url { get; }

    public UrlResponse(string url)
    {
        Url = url;
    }
}