using Microsoft.AspNetCore.Mvc;

namespace ShortLink.API.Controllers;

[ApiController]
public class UrlController : ControllerBase
{
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