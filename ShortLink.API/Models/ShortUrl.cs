namespace ShortLink.API.Models;

public class ShortUrl
{
    public string OriginalUrl { get; }
    public string EncodedUrl { get; }
    public DateTime CreatedAt { get; }

    public ShortUrl(string originalUrl, string encodedUrl)
    {
        OriginalUrl = originalUrl;
        EncodedUrl = encodedUrl;
        CreatedAt = DateTime.UtcNow;
    }
}