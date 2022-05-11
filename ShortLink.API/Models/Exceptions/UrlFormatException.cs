namespace ShortLink.API.Models.Exceptions;

public class UrlFormatException : ApiException
{
    public UrlFormatException()
        : base("The supplied URL was not formatted correctly.", StatusCodes.Status422UnprocessableEntity)
    {
    }
}