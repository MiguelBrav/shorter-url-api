
namespace ShorterAPI.DTO.Responses;

public class ShortyTopResponse
{
    public int Id { get; set; }

    public string ShortUrl { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public int AccessCount { get; set; }
}

