using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using ShortLink.API.Models;
using ShortLink.API.Repositories;
using Xunit;

namespace ShortLink.API.Tests.Repositories;

public class UrlRepositoryTests
{
    private readonly UrlRepository _urlRepository = new();

    public UrlRepositoryTests()
    {
        UrlRepository.SetUnitTestData(new Dictionary<string, ShortUrl>
        {
            { "bar", new ShortUrl("foo", "bar") }
        });
    }
    
    #region GetByCode

    [Theory]
    [InlineData("bar", "foo"), InlineData("zing", null)]
    public async Task GetByCode_Returns_ShortUrl(string code, string url)
    {
        var result = await _urlRepository.GetByCode(code);

        result?.OriginalUrl.Should().Be(url);
    }

    #endregion
    
    #region CreateShortUrl

    [Theory]
    [InlineData("foo", "bar")]
    public async Task CreateShortUrl_Returns_ShortUrl(string url, string code)
    {
        var result = await _urlRepository.CreateShortUrl(url, code);

        result.OriginalUrl.Should().Be(url);
        result.Code.Should().Be(code);
        result.CreatedAt.Should().BeWithin(TimeSpan.FromSeconds(1));
    }
    
    #endregion
    
    #region GenerateCode

    [Theory]
    [InlineData(4), InlineData(6)]
    public async Task GenerateCode_Returns_Code_OfSpecifiedLength(byte length)
    {
        var result = await _urlRepository.GenerateCode(length);

        result.Should().NotBeNullOrWhiteSpace();
        result.Length.Should().Be(length);
    }
    
    #endregion

    #region GetAll

    [Fact]
    public async Task GetAll_Returns_CollectionOfShortUrl()
    {
        var result = await _urlRepository.GetAll();

        result.Should().NotBeNull();
    }

    #endregion
}