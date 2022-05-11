using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ShortLink.API.Models.Exceptions;
using Xunit;

namespace ShortLink.API.Models.Tests.Exceptions;

public class NotFoundExceptionTests
{
    [Fact]
    public void NotFoundException_Constructs_WithNoParameters()
    {
        var ex = new NotFoundException();

        ex.Message.Should().Be("Requested entity could not be found.");
        ex.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}