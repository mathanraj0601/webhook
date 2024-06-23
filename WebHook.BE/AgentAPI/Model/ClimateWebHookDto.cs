namespace AgentAPI.Model
{
    public class ClimateWebHookDto
    {
        public string? WebHookType { get; set; }
        public string? Secret { get; set; }
        public string? Area { get; set; }
        public double OldTemp { get; set; }
        public double NewTemp { get; set; }
        public string? Publisher { get; set; }
    }
}
