using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShorterAPI.DTO.Entities;

public class LogRedirect
{
    [Key]
    public int Id { get; set; }

    public DateTime RedirectDate { get; set; }

    [Required]
    public int ShortyId { get; set; } 

    [ForeignKey(nameof(ShortyId))]
    public Shorty Shorty { get; set; } 

}
