using System.ComponentModel.DataAnnotations;

namespace ClimateAPI.Model.DTO
{
    public class ClimateResponseDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Area { get; set; }
        [Required]
        public double Temp { get; set; }
    }
}
