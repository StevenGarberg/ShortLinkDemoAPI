using ShortLink.API.Models;

namespace ShortLink.API.Repositories;

public interface IUrlRepository
{
    /// <summary>
    /// Gets a Short Url by Code and returns null if it does not exist
    /// </summary>
    /// <param name="url">URL</param>
    /// <returns>Short Url</returns>
    Task<ShortUrl> GetByCode(string url);
    
    /// <summary>
    /// Creates a Short Url from URL and Code which is then returned
    /// </summary>
    /// <param name="url">URL</param>
    /// <param name="code">Code</param>
    /// <returns>Short Url</returns>
    Task<ShortUrl> CreateShortUrl(string url, string code);
    
    /// <summary>
    /// Generates a case sensitive alphanumeric code of the supplied length
    /// </summary>
    /// <param name="length">Optional length, defaults to 6</param>
    /// <returns>Code string</returns>
    Task<string> GenerateCode(int length = 6);
}

public class UrlRepository : IUrlRepository
{
    private static readonly IDictionary<string, ShortUrl> Data = new Dictionary<string, ShortUrl>();

    public async Task<ShortUrl> GetByCode(string code)
    {
        Data.TryGetValue(code, out var shortUrl);
        return await Task.FromResult(shortUrl);
    }

    public async Task<ShortUrl> CreateShortUrl(string url, string code)
    {
        var shortUrl = new ShortUrl(url, code);
        Data.Add(code, shortUrl);
        return await Task.FromResult(shortUrl);
    }

    public async Task<string> GenerateCode(int length = 6)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        
        var code = new char[length];
        var random = new Random();
        
        for (var i = 0; i < length; i++)
        {
            code[i] = characters[random.Next(characters.Length)];
        }
        
        return await Task.FromResult(new string(code));
    }
}