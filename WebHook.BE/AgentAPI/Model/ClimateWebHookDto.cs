namespace AgentAPI.Model
{
    public class ClimateWebHookDto
    {
        public string? WebHookType { get; set; }
        public string? Secret { get; set; }
        public string? Area { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public string? Publisher { get; set; }
    }
}
