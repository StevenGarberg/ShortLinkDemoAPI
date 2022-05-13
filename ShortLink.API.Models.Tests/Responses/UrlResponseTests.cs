using FluentAssertions;
using ShortLink.API.Models.Responses;
using Xunit;

namespace ShortLink.API.Models.Tests.Responses;

public class UrlResponseTests
{
    [Theory]
    [InlineData("https://www.google.com"), InlineData("https://www.google.com/search")]
    public void UrlResponse_Constructs_FromMessage(string url)
    {
        var response = new UrlResponse(url);
        
        response.Url.Should().Be(url);
    }
}