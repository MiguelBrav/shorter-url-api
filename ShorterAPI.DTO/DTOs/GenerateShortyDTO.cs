using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.DTOs;

public class GenerateShortyDTO
{
    [Required]
    public string FullUrl { get; set; }

    [Required]
    [Range(2, int.MaxValue, ErrorMessage = "Length must be at least 2.")]
    public int? Length { get; set; }
}
