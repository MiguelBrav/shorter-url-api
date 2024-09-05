using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.Entities;

public class Shorty
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    [Required]
    [Url]
    public string FullUrl { get; set; } = string.Empty;

    [StringLength(200)]
    public string ShortUrl { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; } = false;

    [Required]
    public string CreatedUser { get; set; } = string.Empty;

    // Constructor
    public Shorty(string title, string fullUrl, string createdUser, string? description = null, string? shortUrl = null)
    {
        Title = title;
        FullUrl = fullUrl;
        CreatedUser = createdUser;
        Description = description ?? string.Empty;
        ShortUrl = shortUrl ?? string.Empty;
        CreatedDate = DateTime.UtcNow;
    }
}
