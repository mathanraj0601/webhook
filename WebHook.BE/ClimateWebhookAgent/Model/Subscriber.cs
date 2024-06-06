using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateWebhookAgent.Model
{
    public class Subscriber
    {
        [Key]
        public int Id { get; set; }
        public string? SubscriberUrl { get; set; }
        public Guid Secret { get; set; }
        public string? WebHookType { get; set; }
        public string? Publisher { get; set; }

    }
}
