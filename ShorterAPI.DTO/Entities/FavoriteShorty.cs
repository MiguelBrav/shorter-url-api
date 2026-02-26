using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.Entities;

public class FavoriteShorty
{

    [Required]
    public int ShortyId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public Shorty? Shorty { get; set; }

    public FavoriteShorty() { }
    public FavoriteShorty(int shortyId, string userId)
    {
        ShortyId = shortyId;
        UserId = userId;
        CreatedDate = DateTime.UtcNow;
    }
}

