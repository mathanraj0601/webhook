using System.ComponentModel.DataAnnotations;

namespace ClimateAPI.Model
{
    public class Subscriber
    {
        [Key]
        public int Id { get; set; }
        public string? SubscriberUrl { get; set; }
        public Guid Secret { get; set; }
        public string? WebHookType { get; set; }
        public string? Publisher { get; set;}

    }
}
