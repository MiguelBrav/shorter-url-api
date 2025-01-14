using ShorterAPI.DTO.Entities;

namespace ShorterAPI.DTO.Responses;

public class ShortyStatsResponse
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string ShortUrl { get; set; } = string.Empty;

    public string FullUrl { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int AccessCount { get; set; }

    public ShortyStatsResponse() { }
    public ShortyStatsResponse(Shorty shorty, int accessCount)
    {
        Id = shorty.Id;
        Title = shorty.Title;
        ShortUrl = shorty.ShortUrl;
        FullUrl = shorty.FullUrl;
        CreatedDate = shorty.CreatedDate;
        UpdatedDate = shorty.UpdatedDate;
        AccessCount = accessCount;
    }
}

