using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.DTOs;

public class ShortyUrlDTO
{
    [Required]
    public string ShortyName { get; set; }
}
