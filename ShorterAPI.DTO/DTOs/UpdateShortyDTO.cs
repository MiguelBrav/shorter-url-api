
namespace ShorterAPI.DTO.DTOs;

public class UpdateShortyDTO
{
    public int Id { get; set; }
    public string FullUrl { get; set; }
    public string ShortUrl { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; } 
}
