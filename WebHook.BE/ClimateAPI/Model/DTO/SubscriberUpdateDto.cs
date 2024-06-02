using System.ComponentModel.DataAnnotations;

namespace ClimateAPI.Model.DTO
{
    public class SubscriberUpdateDto
    {
        public int Id { get; set; }
        public string? SubscriberUrl { get; set; }
 
    }
}
