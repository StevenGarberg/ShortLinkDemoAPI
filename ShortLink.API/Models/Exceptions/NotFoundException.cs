namespace ShortLink.API.Models.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException()
        : base("Requested entity could not be found.", StatusCodes.Status404NotFound)
    {
    }
}