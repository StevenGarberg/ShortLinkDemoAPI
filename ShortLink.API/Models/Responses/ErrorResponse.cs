namespace ShortLink.API.Models.Responses;

public class ErrorResponse
{
    public string Message { get; }

    public ErrorResponse(Exception e)
    {
        Message = e?.Message ?? "An error has occurred.";
    }
}