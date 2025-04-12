
namespace ShorterAPI.DTO.Responses;

public class LastShortyResponse
{
    public int Id { get; set; }

    public string ShortUrl { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

}

