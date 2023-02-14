using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MyBGList.Models;

[Table("Categories")]
public class Catergory
{
    [Key] [Required] public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime LastModifiedDate { get; set; }
    
    // Navigation Property
    public ICollection<BoardGames_Categories>? BoardGamesCategoriesCollection { get; set; }
}