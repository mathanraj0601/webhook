using System.ComponentModel.DataAnnotations;

namespace ClimateAPI.Model.DTO
{
    public class ClimateCreateDto
    {
        [Required]
        public string? Area { get; set; }
        [Required]
        public double Temp { get; set; }
    }
}
