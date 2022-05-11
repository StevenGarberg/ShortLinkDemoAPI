using Microsoft.AspNetCore.Mvc;
using ShortLink.API.Services;

namespace ShortLink.API.Controllers;

[ApiController]
public class UrlController : ControllerBase
{
    private readonly IUrlService _urlService;
    
    public UrlController(IUrlService urlService)
    {
        _urlService = urlService;
    }
    
    [HttpPost("encode")]
    public Task<IActionResult> Encode()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("decode")]
    public Task<IActionResult> Decode()
    {
        throw new NotImplementedException();
    }
}