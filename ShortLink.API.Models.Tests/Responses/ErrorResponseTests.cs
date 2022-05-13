using System;
using FluentAssertions;
using ShortLink.API.Models.Responses;
using Xunit;

namespace ShortLink.API.Models.Tests.Responses;

public class ErrorResponseTests
{
    [Theory]
    [InlineData("Generic exception message"), InlineData("")]
    public void ErrorResponse_Constructs_FromException(string message)
    {
        var e = new Exception(message);

        var errorResponse = new ErrorResponse(e);

        errorResponse.Should().NotBeNull();
        errorResponse.Message.Should().Be(message);
    }
    
    [Fact]
    public void ErrorResponse_Constructs_FromNullException()
    {
        var errorResponse = new ErrorResponse(null);

        errorResponse.Should().NotBeNull();
        errorResponse.Message.Should().Be("An error has occurred.");
    }
}