using ShorterAPI.DTO.Entities;

namespace ShorterAPI.DTO.Responses;

public class FavoriteShortyResponse
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ShortUrl { get; set; } = string.Empty;

    public string FullUrl { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }  
}

