using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShortLink.API.Controllers;
using ShortLink.API.Models;
using ShortLink.API.Models.Requests;
using ShortLink.API.Models.Responses;
using ShortLink.API.Services;
using Xunit;

namespace ShortLink.API.Tests.Controllers;

public class UrlControllerTests
{
    private readonly UrlController _urlController;
    private readonly Mock<IUrlService> _urlService = new();
    
    public UrlControllerTests()
    {
        _urlService.Setup(x => x.Encode(It.IsAny<UrlRequest>())).ThrowsAsync(new Exception());
        _urlService.Setup(x => x.Decode(It.IsAny<UrlRequest>())).ThrowsAsync(new Exception());
        _urlService.Setup(x => x.GetAll()).ReturnsAsync(Array.Empty<ShortUrl>());
        
        _urlController = new UrlController(_urlService.Object);
    }

    #region Encode
    
    [Theory]
    [InlineData("https://www.google.com", "foo")]
    public async Task Encode_Returns_OkResult_ContainingUrlResponse_WhenRequestIsValid(string url, string encodedUrl)
    {
        var request = new UrlRequest { Url = url };
        
        _urlService.Setup(x => x.Encode(request)).ReturnsAsync(new UrlResponse(encodedUrl));
        
        var result = await _urlController.Encode(request) as ObjectResult;
        var value = result!.Value as UrlResponse;

        result.StatusCode.Should().Be(StatusCodes.Status200OK);
        value.Should().NotBeNull();
        value!.Url.Should().Be(encodedUrl);
    }
    
    [Fact]
    public async Task Encode_Returns_ObjectResult_ContainingErrorResponse_WhenRequestIsInvalid()
    {
        var request = new UrlRequest { Url = null };
        
        var result = await _urlController.Encode(request) as ObjectResult;
        var value = result!.Value as ErrorResponse;

        result.StatusCode.Should().NotBe(StatusCodes.Status200OK);
        value.Should().NotBeNull();
        value!.Message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Encode_Calls_OwnerRepository()
    {
        var request = new UrlRequest { Url = "any" };
        
        await _urlController.Encode(request);
        
        _urlService.Verify(x => x.Encode(request), Times.Once);
    }
    
    #endregion
    
    #region Decode
    
    [Theory]
    [InlineData("foo", "www.https://google.com")]
    public async Task Decode_Returns_OkResult_ContainingUrlResponse_WhenRequestIsValid(string encodedUrl, string decodedUrl)
    {
        var request = new UrlRequest { Url = encodedUrl };
        
        _urlService.Setup(x => x.Decode(request)).ReturnsAsync(new UrlResponse(decodedUrl));
        
        var result = await _urlController.Decode(request) as ObjectResult;
        var value = result!.Value as UrlResponse;

        result.StatusCode.Should().Be(StatusCodes.Status200OK);
        value.Should().NotBeNull();
        value!.Url.Should().Be(decodedUrl);
    }
    
    [Fact]
    public async Task Decode_Returns_ObjectResult_ContainingErrorResponse_WhenRequestIsInvalid()
    {
        var request = new UrlRequest { Url = null };
        
        var result = await _urlController.Decode(request) as ObjectResult;
        var value = result!.Value as ErrorResponse;

        result.StatusCode.Should().NotBe(StatusCodes.Status200OK);
        value.Should().NotBeNull();
        value!.Message.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Decode_Calls_OwnerRepository()
    {
        var request = new UrlRequest { Url = "any" };
        
        await _urlController.Decode(request);
        
        _urlService.Verify(x => x.Decode(request), Times.Once);
    }
    
    #endregion
    
    #region GetAll
    
    [Fact]
    public async Task GetAll_Returns_OkResult_Containing_CollectionOfShortUrl()
    {
        var result = await _urlController.GetAll() as ObjectResult;
        var value = result!.Value as IReadOnlyCollection<ShortUrl>;

        result.StatusCode.Should().Be(StatusCodes.Status200OK);
        value.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetAll_Calls_UrlService()
    {
        await _urlController.GetAll();
        
        _urlService.Verify(x => x.GetAll(), Times.Once);
    }
    
    #endregion
}