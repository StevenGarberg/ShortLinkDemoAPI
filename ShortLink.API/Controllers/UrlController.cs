using Microsoft.AspNetCore.Mvc;
using ShortLink.API.Models;
using ShortLink.API.Models.Exceptions;
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
    [ProducesResponseType(typeof(UrlResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Encode([FromBody] UrlRequest request)
    {
        return await HandleAction(
            _urlService.Encode(request)
        );
    }
    
    [HttpPost("decode")]
    [ProducesResponseType(typeof(UrlResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Decode([FromBody] UrlRequest request)
    {
        return await HandleAction(
            _urlService.Decode(request)
        );
    }

    [HttpGet("fetch")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ShortUrl>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return await HandleAction(
            _urlService.GetAll()
        );
    }

    private async Task<IActionResult> HandleAction<T>(Task<T> task) where T : class
    {
        try
        {
            return Ok(await task);
        }
        catch(Exception e)
        {
            return new ObjectResult(new ErrorResponse(e))
            {
                StatusCode = e is ApiException apiException ? apiException.StatusCode : StatusCodes.Status400BadRequest
            };
        }
    }
}