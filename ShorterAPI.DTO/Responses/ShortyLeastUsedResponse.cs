
namespace ShorterAPI.DTO.Responses;

public class ShortyLeastUsedResponse
{
    public int Id { get; set; }

    public string ShortUrl { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public int AccessCount { get; set; }
}

