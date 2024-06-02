using System.ComponentModel.DataAnnotations;

namespace ClimateAPI.Model.DTO
{
    public class ClimateUpdateDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Temp { get; set; }
    }
}
