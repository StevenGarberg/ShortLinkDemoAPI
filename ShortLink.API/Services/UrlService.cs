using ShortLink.API.Models.Requests;

namespace ShortLink.API.Services;

public interface IUrlService
{
    Task<string> Encode(UrlRequest request);
    Task<string> Decode(UrlRequest request);
}

public class UrlService : IUrlService
{
    public Task<string> Encode(UrlRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<string> Decode(UrlRequest request)
    {
        throw new NotImplementedException();
    }
}