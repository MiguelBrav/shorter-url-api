namespace ShorterAPI.DTO.DTOs;

public class CreateShortyDTO
{
    public string FullUrl { get; set; }
    public string ShortUrl { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; } 
}
