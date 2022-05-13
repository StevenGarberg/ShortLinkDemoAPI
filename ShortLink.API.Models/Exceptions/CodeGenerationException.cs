using Microsoft.AspNetCore.Http;

namespace ShortLink.API.Models.Exceptions;

public class CodeGenerationException : ApiException
{
    public CodeGenerationException()
        : base("An error occurred while encoding the supplied URL.", StatusCodes.Status422UnprocessableEntity)
    {
    }
}