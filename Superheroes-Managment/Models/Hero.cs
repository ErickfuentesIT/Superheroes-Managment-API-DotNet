using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Superheroes_Managment.Models
{
    [Table("Heroes")]
    public class Hero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Alias { get; set; }

    }
}
