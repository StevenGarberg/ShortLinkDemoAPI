using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ShortLink.API.Models;
using ShortLink.API.Models.Exceptions;
using ShortLink.API.Models.Requests;
using ShortLink.API.Repositories;
using ShortLink.API.Services;
using Xunit;

namespace ShortLink.API.Tests.Services;

public class UrlServiceTests
{
    private readonly UrlService _urlService;
    private readonly Mock<IUrlRepository> _urlRepository = new();

    public UrlServiceTests()
    {
        _urlRepository.Setup(x => x.GenerateCode(6)).ReturnsAsync("bar");
        _urlRepository.Setup(x => x.GetAll()).ReturnsAsync(Array.Empty<ShortUrl>());

        _urlService = new UrlService(_urlRepository.Object);
    }
    
    #region Encode

    [Fact]
    public async Task Encode_Returns_UrlResponse_WhenSuccessful()
    {
        var request = new UrlRequest { Url = "kaz" };
        
        var result = await _urlService.Encode(request);

        result.Should().NotBeNull();
        result.Url.Should().NotBeNullOrWhiteSpace();
        result.Url.Should().NotBe(request.Url);
        result.Url.Should().StartWith("https://");
    }
    
    [Fact]
    public async Task Encode_Throws_CodeGenerationException_WhenCannotGenerateCode()
    {
        _urlRepository.Setup(x => x.GetByCode("bar")).ReturnsAsync(new ShortUrl("foo", "bar"));

        var func = async () => { await _urlService.Encode(new UrlRequest()); };

        await func.Should().ThrowAsync<CodeGenerationException>();
    }
    
    [Fact]
    public async Task Encode_Calls_UrlRepository()
    {
        var request = new UrlRequest { Url = "kaz" };
        
        await _urlService.Encode(request);

        _urlRepository.Verify(x => x.GenerateCode(It.IsAny<byte>()), Times.Once);
        _urlRepository.Verify(x => x.CreateShortUrl(request.Url, It.IsAny<string>()), Times.Once);
    }
    
    #endregion
    
    #region Decode

    [Fact]
    public async Task Decode_Throws_NotFoundException_WhenUrlNotFound()
    {
        _urlRepository.Setup(x => x.GetByCode("bar")).ReturnsAsync(new ShortUrl("foo", "bar"));

        var func = async () => { await _urlService.Decode(new UrlRequest { Url = "kaz" }); };

        await func.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task Decode_Calls_UrlRepository()
    {
        var request = new UrlRequest { Url = $"{Constants.BaseUrl}foo" };
        
        await _urlService.Decode(request);

        _urlRepository.Verify(x => x.GetByCode(It.IsAny<string>()), Times.Once);
    }
    
    #endregion
    
    #region GetAll

    [Fact]
    public async Task GetAll_Returns_CollectionOfShortUrl()
    {
        var result = await _urlService.GetAll();

        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetAll_Calls_UrlRepository()
    {
        var result = await _urlService.GetAll();

        _urlRepository.Verify(x => x.GetAll(), Times.Once);
    }

    #endregion
}