using System.ComponentModel.DataAnnotations;

namespace ShorterAPI.DTO.Entities;

public class Tag
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public Tag(string name)
    {
        Name = name.ToLower().Trim();
        CreatedDate = DateTime.UtcNow;
    }

    public Tag() { }
}