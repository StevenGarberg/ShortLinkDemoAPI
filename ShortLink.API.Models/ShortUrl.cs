namespace ShortLink.API.Models;

public class ShortUrl
{
    public string OriginalUrl { get; }
    public string Code { get; }
    public DateTime CreatedAt { get; }

    public ShortUrl(string originalUrl, string code)
    {
        OriginalUrl = originalUrl;
        Code = code;
        CreatedAt = DateTime.UtcNow;
    }
}