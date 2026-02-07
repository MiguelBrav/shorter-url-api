using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.DTOs;

public class CloneShortyDTO
{
    [Required]
    public int Id { get; set; }

    public string ShortUrl { get; set; } = string.Empty;
}
