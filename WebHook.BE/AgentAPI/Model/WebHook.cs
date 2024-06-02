using System.ComponentModel.DataAnnotations;

namespace AgentAPI.Model
{
    public class WebHook
    {
        [Key]
        public int Id { get; set; }
        public string? WebHookType { get; set; }
        public string? Secret { get; set; }
        public string? Publisher { get; set; }
    }
}
