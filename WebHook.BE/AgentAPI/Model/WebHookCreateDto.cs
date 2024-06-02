using System.ComponentModel.DataAnnotations;

namespace AgentAPI.Model
{
    public class WebHookCreateDto
    {
        public string? WebHookType { get; set; }
        public string? Secret { get; set; }
        public string? Publisher { get; set; }
    }
}
