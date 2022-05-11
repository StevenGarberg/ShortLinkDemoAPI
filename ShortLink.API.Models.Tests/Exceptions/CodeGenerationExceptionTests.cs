using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ShortLink.API.Models.Exceptions;
using Xunit;

namespace ShortLink.API.Models.Tests.Exceptions;

public class CodeGenerationExceptionTests
{
    [Fact]
    public void CodeGenerationException_Constructs_WithNoParameters()
    {
        var ex = new CodeGenerationException();

        ex.Message.Should().Be("An error occurred while encoding the supplied URL.");
        ex.StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
    }
}