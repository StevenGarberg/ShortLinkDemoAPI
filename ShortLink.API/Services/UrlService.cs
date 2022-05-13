using ShortLink.API.Models;
using ShortLink.API.Models.Exceptions;
using ShortLink.API.Models.Requests;
using ShortLink.API.Models.Responses;
using ShortLink.API.Repositories;

namespace ShortLink.API.Services;

public interface IUrlService
{
    /// <summary>
    /// Encodes a user supplied URL
    /// </summary>
    /// <param name="request">URL Request</param>
    /// <returns>URL Response</returns>
    Task<UrlResponse> Encode(UrlRequest request);
    
    /// <summary>
    /// Decodes a user supplied URL, if it exists in the data store
    /// </summary>
    /// <param name="request">URL Request</param>
    /// <returns>URL Response</returns>
    Task<UrlResponse> Decode(UrlRequest request);

    /// <summary>
    /// Retrieves all Short URLs
    /// </summary>
    /// <returns>Collection of Short Url</returns>
    Task<IReadOnlyCollection<ShortUrl>> GetAll();
}

public class UrlService : IUrlService
{
    private readonly IUrlRepository _urlRepository;
    
    public UrlService(IUrlRepository urlRepository)
    {
        _urlRepository = urlRepository;
    }
    
    public async Task<UrlResponse> Encode(UrlRequest request)
    {
        string code = null;
        ShortUrl shortUrl = null;
        
        for (var i = 0; i < 10000; i++)
        {
            code = await _urlRepository.GenerateCode();
            shortUrl = await _urlRepository.GetByCode(code);
            if (shortUrl == null)
                break;
        }
        
        if (shortUrl != null) throw new CodeGenerationException();
        
        await _urlRepository.CreateShortUrl(request.Url, code);
        
        return new UrlResponse($"https://{Constants.BaseUrl}{code}");
    }

    public async Task<UrlResponse> Decode(UrlRequest request)
    {
        string code;
        try
        {
            code = request.Url.Split(Constants.BaseUrl)[1];
        }
        catch
        {
            throw new UrlFormatException();
        }
        
        var shortUrl = await _urlRepository.GetByCode(code);
        
        if (shortUrl == null) throw new NotFoundException();
        
        return new UrlResponse(shortUrl.OriginalUrl);
    }

    public async Task<IReadOnlyCollection<ShortUrl>> GetAll()
    {
        return await _urlRepository.GetAll();
    }
}