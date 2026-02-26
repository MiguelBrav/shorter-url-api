using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.Entities;

public class ShortyTag
{
    [Key]
    public int Id { get; set; }

    public int ShortyId { get; set; }
    public int TagId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    // Navigation property 
    public Tag? Tag { get; set; }
}
