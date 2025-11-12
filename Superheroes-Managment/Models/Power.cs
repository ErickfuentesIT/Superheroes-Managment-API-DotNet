using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Superheroes_Managment.Models
{
    [Table("Powers")]
    public class Power
    {
        [Key]
        public int Id { get; set; }
        public required int HeroId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [ForeignKey("HeroId")]
        [JsonIgnore]
        public Hero? Hero { get; set; }
    }
}
