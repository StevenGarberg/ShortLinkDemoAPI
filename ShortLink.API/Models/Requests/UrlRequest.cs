using System.ComponentModel.DataAnnotations;

namespace ShortLink.API.Models.Requests;

public class UrlRequest
{
    [Required(AllowEmptyStrings = false), MinLength(3), MaxLength(100)]
    public string Url { get; set; }
}