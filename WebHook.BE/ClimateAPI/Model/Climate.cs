using System.ComponentModel.DataAnnotations;

namespace ClimateAPI.Model
{
    public class Climate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Area { get; set; }
        [Required]
        public double Temp { get; set; }

    }
}
