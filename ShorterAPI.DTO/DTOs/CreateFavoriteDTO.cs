
using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.DTOs;

public class CreateFavoriteDTO
{
    [Required]
    public int ShortyId { get; set; }
}
