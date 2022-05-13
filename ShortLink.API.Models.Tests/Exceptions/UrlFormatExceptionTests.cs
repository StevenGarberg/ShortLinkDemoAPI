using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ShortLink.API.Models.Exceptions;
using Xunit;

namespace ShortLink.API.Models.Tests.Exceptions;

public class UrlFormatExceptionTests
{
    [Fact]
    public void UrlFormatException_Constructs_WithNoParameters()
    {
        var ex = new UrlFormatException();

        ex.Message.Should().Be("The supplied URL was not formatted correctly.");
        ex.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
    }
}