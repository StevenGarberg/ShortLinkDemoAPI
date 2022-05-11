using Microsoft.AspNetCore.Mvc;
using ShortLink.API.Models.Requests;
using ShortLink.API.Models.Responses;
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
    public async Task<IActionResult> Encode([FromBody] UrlRequest request)
    {
        try
        {
            return Ok(await _urlService.Encode(request));
        }
        catch(Exception e)
        {
            return new ObjectResult(new ErrorResponse(e))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
    
    [HttpPost("decode")]
    public async Task<IActionResult> Decode([FromBody] UrlRequest request)
    {
        try
        {
            return Ok(await _urlService.Decode(request));
        }
        catch(Exception e)
        {
            return new ObjectResult(new ErrorResponse(e))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}