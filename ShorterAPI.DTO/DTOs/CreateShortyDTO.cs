using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.DTOs;

public class CreateShortyDTO
{
    [Required]
    public string FullUrl { get; set; }

    [Required]
    public string ShortUrl { get; set; }

    [Required]
    public string Title { get; set; }
    public string? Description { get; set; } 
}
