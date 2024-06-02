using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateWebhookAgent.Model
{
    public class WebHookWorkerDto
    {
        public string? WebHookType { get; set; }
        public string? Publisher { get; set; }
        public double OldTemp { get; set; }
        public double NewTemp { get; set; }
        public string? Area { get; set; }
    }
}
