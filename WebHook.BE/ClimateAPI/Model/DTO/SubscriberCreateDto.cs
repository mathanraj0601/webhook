using System.ComponentModel.DataAnnotations;

namespace ClimateAPI.Model.DTO
{
    public class SubscriberCreateDto
    {
        public string? SubscriberUrl { get; set; }
        public Guid Secret { get; set; }
        public string? WebHookType { get; set; }
        public string? Publisher { get; set; }
    }
}
